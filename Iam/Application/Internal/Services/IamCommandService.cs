using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Iam.Application.Internal.OutboundServices;
using RiskGuard.Platform.Iam.Domain.Model.Aggregates;
using RiskGuard.Platform.Iam.Domain.Model.Commands;
using RiskGuard.Platform.Shared.Application.Model;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.Iam.Application.Internal.Services;

public class IamCommandService(
    AppDbContext context,
    IHashingService hashingService,
    ITokenService tokenService,
    IUnitOfWork unitOfWork)
{
    private static readonly TimeSpan LockoutDuration = TimeSpan.FromMinutes(15);

    public async Task<Result<(User user, string token)>> Handle(SignInCommand command, string ipAddress,
        CancellationToken cancellationToken)
    {
        var email = command.Email.Trim().ToLowerInvariant();
        var user = await context.Users.FirstOrDefaultAsync(item => item.Email == email, cancellationToken);
        if (user is null)
            return Result<(User user, string token)>.Failure("InvalidCredentials", "Correo o contrasena incorrectos");

        if (user.LockedUntil is not null && user.LockedUntil > DateTime.UtcNow)
            return Result<(User user, string token)>.Failure("AccountLocked",
                "Demasiados intentos fallidos. Intente en 15 minutos");

        if (!hashingService.VerifyPassword(command.Password, user.PasswordHash))
        {
            user.FailedAttempts++;
            if (user.FailedAttempts >= 5)
            {
                user.LockedUntil = DateTime.UtcNow.Add(LockoutDuration);
                user.AccountStatus = "LOCKED";
            }

            context.AccessLogs.Add(new AccessLog
            {
                UserId = user.Id,
                Email = email,
                IpAddress = ipAddress,
                WasSuccessful = false,
                FailureReason = "Invalid credentials"
            });
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<(User user, string token)>.Failure("InvalidCredentials", "Correo o contrasena incorrectos");
        }

        user.FailedAttempts = 0;
        user.LockedUntil = null;
        user.AccountStatus = "ACTIVE";
        var token = tokenService.GenerateToken(user);
        context.Sessions.Add(new Session
        {
            UserId = user.Id,
            TokenSignature = token[^Math.Min(token.Length, 64)..],
            IsValid = true
        });
        context.AccessLogs.Add(new AccessLog
        {
            UserId = user.Id,
            Email = email,
            IpAddress = ipAddress,
            WasSuccessful = true
        });
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<(User user, string token)>.Success((user, token));
    }

    public async Task<Result<User>> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var email = command.Email.Trim().ToLowerInvariant();
        var exists = await context.Users.AnyAsync(user => user.Email == email, cancellationToken);
        if (exists) return Result<User>.Failure("EmailAlreadyTaken", "El correo ya esta registrado");

        var user = new User
        {
            Name = command.Name.Trim(),
            Email = email,
            Role = NormalizeRole(command.Role),
            RoleId = RoleIdFor(command.Role),
            SiteAreaId = command.SiteAreaId,
            PasswordHash = hashingService.HashPassword(command.Password),
            AccountStatus = "ACTIVE"
        };
        context.Users.Add(user);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<User>.Success(user);
    }

    private static string NormalizeRole(string role)
    {
        return role.ToUpperInvariant() switch
        {
            "OPERATOR" or "OPERARIO" or "PLANT-OPERATOR" => "Operario",
            "SUPERVISOR" => "Supervisor",
            "MANAGER" or "ADMIN" or "ADMINISTRADOR" => "Administrador",
            _ => role
        };
    }

    private static string RoleIdFor(string role)
    {
        return NormalizeRole(role) switch
        {
            "Operario" => "c1a42619-e994-48f0-92c6-cdf0a104e7a1",
            "Supervisor" => "4afbd60b-8da6-46a6-9b48-2d04b8fa2161",
            "Administrador" => "af74b6d7-8217-44d6-b8d1-9bd5cd7555d2",
            _ => string.Empty
        };
    }
}
