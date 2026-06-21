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

public class TicketHistoryCommandService(ITicketHistoryRepository repository, IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessage> localizer) : ITicketHistoryCommandService
{
    public async Task<Result<TicketHistory>> Handle(CreateTicketHistoryCommand command, CancellationToken cancellationToken)
    {
        var entity = new TicketHistory
        {
            TicketId = command.TicketId,
            Event = command.Event,
            UserId = command.UserId,
            UserName = command.UserName,
            Details = command.Details,
            Date = command.Date
        };
        try
        {
            await repository.AddAsync(entity, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<TicketHistory>.Success(entity);
        }
        catch (OperationCanceledException)
        {
            return Result<TicketHistory>.Failure(TicketHistoryError.OperationCancelled, localizer[nameof(TicketHistoryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<TicketHistory>.Failure(TicketHistoryError.DatabaseError, localizer[nameof(TicketHistoryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<TicketHistory>.Failure(TicketHistoryError.InternalServerError, localizer[nameof(TicketHistoryError.InternalServerError)]);
        }
    }
}
