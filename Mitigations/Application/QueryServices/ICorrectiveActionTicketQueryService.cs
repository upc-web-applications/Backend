using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;

namespace Acme.Center.Platform.Mitigations.Application.QueryServices;

public interface ICorrectiveActionTicketQueryService
{
    Task<IEnumerable<CorrectiveActionTicket>> Handle(GetAllCorrectiveActionTicketsQuery query, CancellationToken cancellationToken);
    Task<CorrectiveActionTicket?> Handle(GetCorrectiveActionTicketByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<CorrectiveActionTicket>> Handle(GetTicketsBySectorQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<CorrectiveActionTicket>> Handle(GetTicketsByStatusQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<CorrectiveActionTicket>> Handle(GetTicketsByTechnicianQuery query, CancellationToken cancellationToken);
}
