using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.Mitigations.Application.CommandServices;

public interface ICorrectiveActionTicketCommandService
{
    Task<Result<CorrectiveActionTicket>> Handle(CreateCorrectiveActionTicketCommand command, CancellationToken cancellationToken);
}
