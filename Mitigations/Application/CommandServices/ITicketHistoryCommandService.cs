using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.Mitigations.Application.CommandServices;

public interface ITicketHistoryCommandService
{
    Task<Result<TicketHistory>> Handle(CreateTicketHistoryCommand command, CancellationToken cancellationToken);
}
