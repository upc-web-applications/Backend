using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Acme.Center.Platform.Iam.Application.CommandServices;
using Acme.Center.Platform.Iam.Application.Internal.OutboundServices;
using Acme.Center.Platform.Iam.Domain.Model;
using Acme.Center.Platform.Iam.Domain.Model.Aggregates;
using Acme.Center.Platform.Iam.Domain.Model.Commands;
using Acme.Center.Platform.Iam.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Resources.Errors;

namespace Acme.Center.Platform.Iam.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer) : IUserCommandService
{
    public async Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username, cancellationToken);
        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User user, string token)>.Failure(
                IamError.InvalidCredentials, localizer[nameof(IamError.InvalidCredentials)]);

        var token = tokenService.GenerateToken(user);
        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByUsernameAsync(command.Username, cancellationToken))
            return Result.Failure(IamError.UsernameAlreadyTaken, localizer[nameof(IamError.UsernameAlreadyTaken), command.Username]);

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(IamError.OperationCancelled, localizer[nameof(IamError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, localizer[nameof(IamError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result.Failure(IamError.InternalServerError, localizer[nameof(IamError.InternalServerError)]);
        }
    }
}
