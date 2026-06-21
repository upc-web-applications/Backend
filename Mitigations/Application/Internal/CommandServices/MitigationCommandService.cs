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

public class MitigationCommandService(IMitigationRepository repository, IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessage> localizer) : IMitigationCommandService
{
    public async Task<Result<Mitigation>> Handle(CreateMitigationCommand command, CancellationToken cancellationToken)
    {
        var entity = new Mitigation
        {
            RiskAssessmentId = command.RiskAssessmentId,
            TicketId = command.TicketId,
            Code = command.Code,
            Description = command.Description,
            Responsible = command.Responsible,
            AssignedDate = command.AssignedDate,
            ExecutionDate = command.ExecutionDate,
            Status = command.Status,
            Result = command.Result,
            Observations = command.Observations
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Mitigation>.Success(entity);
        }
        catch (OperationCanceledException)
        {
            return Result<Mitigation>.Failure(MitigationError.OperationCancelled, localizer[nameof(MitigationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Mitigation>.Failure(MitigationError.DatabaseError, localizer[nameof(MitigationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Mitigation>.Failure(MitigationError.InternalServerError, localizer[nameof(MitigationError.InternalServerError)]);
        }
    }
}
