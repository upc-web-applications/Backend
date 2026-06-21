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

public class CriticalNotificationCommandService(ICriticalNotificationRepository repository, IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessage> localizer) : ICriticalNotificationCommandService
{
    public async Task<Result<CriticalNotification>> Handle(CreateCriticalNotificationCommand command, CancellationToken cancellationToken)
    {
        var entity = new CriticalNotification
        {
            TicketId = command.TicketId,
            SupervisorId = command.SupervisorId,
            SupervisorName = command.SupervisorName,
            Message = command.Message,
            Sent = command.Sent,
            SentDate = command.SentDate
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<CriticalNotification>.Success(entity);
        }
        catch (OperationCanceledException)
        {
            return Result<CriticalNotification>.Failure(CriticalNotificationError.OperationCancelled, localizer[nameof(CriticalNotificationError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<CriticalNotification>.Failure(CriticalNotificationError.DatabaseError, localizer[nameof(CriticalNotificationError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<CriticalNotification>.Failure(CriticalNotificationError.InternalServerError, localizer[nameof(CriticalNotificationError.InternalServerError)]);
        }
    }
}
