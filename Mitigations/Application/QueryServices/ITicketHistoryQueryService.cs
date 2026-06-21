using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;

namespace Acme.Center.Platform.Mitigations.Application.QueryServices;

public interface ITicketHistoryQueryService
{
    Task<IEnumerable<TicketHistory>> Handle(GetAllTicketHistoriesQuery query, CancellationToken cancellationToken);
    Task<TicketHistory?> Handle(GetTicketHistoryByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<TicketHistory>> Handle(GetHistoriesByTicketQuery query, CancellationToken cancellationToken);
}
