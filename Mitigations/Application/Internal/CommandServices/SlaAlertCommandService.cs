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

public class SlaAlertCommandService(ISlaAlertRepository repository, IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessage> localizer) : ISlaAlertCommandService
{
    public async Task<Result<SlaAlert>> Handle(CreateSlaAlertCommand command, CancellationToken cancellationToken)
    {
        var entity = new SlaAlert
        {
            TicketId = command.TicketId,
            ElapsedHours = command.ElapsedHours,
            SlaLimitHours = command.SlaLimitHours,
            AlertDate = command.AlertDate,
            NotifiedTo = command.NotifiedTo,
            NotifiedName = command.NotifiedName
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SlaAlert>.Success(entity);
        }
        catch (OperationCanceledException)
        {
            return Result<SlaAlert>.Failure(SlaAlertError.OperationCancelled, localizer[nameof(SlaAlertError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<SlaAlert>.Failure(SlaAlertError.DatabaseError, localizer[nameof(SlaAlertError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<SlaAlert>.Failure(SlaAlertError.InternalServerError, localizer[nameof(SlaAlertError.InternalServerError)]);
        }
    }
}
