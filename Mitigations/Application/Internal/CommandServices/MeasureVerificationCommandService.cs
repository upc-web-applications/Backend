using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Acme.Center.Platform.Mitigations.Application.CommandServices;
using Acme.Center.Platform.Mitigations.Domain.Model;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Resources.Errors;

namespace Acme.Center.Platform.Mitigations.Application.Internal.CommandServices;

public class MeasureVerificationCommandService(IMeasureVerificationRepository repository, IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessage> localizer) : IMeasureVerificationCommandService
{
    public async Task<Result<MeasureVerification>> Handle(CreateMeasureVerificationCommand command, CancellationToken cancellationToken)
    {
        var entity = new MeasureVerification
        {
            TicketId = command.TicketId,
            SupervisorId = command.SupervisorId,
            SupervisorName = command.SupervisorName,
            Verdict = command.Verdict,
            JustificationComment = command.JustificationComment,
            VerificationDate = command.VerificationDate
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<MeasureVerification>.Success(entity);
        }
        catch (OperationCanceledException)
        {
            return Result<MeasureVerification>.Failure(MeasureVerificationError.OperationCancelled, localizer[nameof(MeasureVerificationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<MeasureVerification>.Failure(MeasureVerificationError.DatabaseError, localizer[nameof(MeasureVerificationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<MeasureVerification>.Failure(MeasureVerificationError.InternalServerError, localizer[nameof(MeasureVerificationError.InternalServerError)]);
        }
    }
}
