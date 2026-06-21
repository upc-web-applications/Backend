using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;
using Acme.Center.Platform.Mitigations.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Application.Internal.QueryServices;

public class TicketHistoryQueryService(ITicketHistoryRepository repository) : ITicketHistoryQueryService
{
    public async Task<IEnumerable<TicketHistory>> Handle(GetAllTicketHistoriesQuery q, CancellationToken ct) => await repository.ListAsync(ct);
    public async Task<TicketHistory?> Handle(GetTicketHistoryByIdQuery q, CancellationToken ct) => await repository.FindByIdAsync(q.Id, ct);
    public async Task<IEnumerable<TicketHistory>> Handle(GetHistoriesByTicketQuery q, CancellationToken ct) => await repository.FindByTicketIdAsync(q.TicketId, ct);
}
