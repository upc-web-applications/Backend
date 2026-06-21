using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;
using Acme.Center.Platform.Mitigations.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Application.Internal.QueryServices;

public class SlaAlertQueryService(ISlaAlertRepository repository) : ISlaAlertQueryService
{
    public async Task<IEnumerable<SlaAlert>> Handle(GetAllSlaAlertsQuery q, CancellationToken ct) => await repository.ListAsync(ct);
    public async Task<SlaAlert?> Handle(GetSlaAlertByIdQuery q, CancellationToken ct) => await repository.FindByIdAsync(q.Id, ct);
    public async Task<IEnumerable<SlaAlert>> Handle(GetAlertsByTicketQuery q, CancellationToken ct) => await repository.FindByTicketIdAsync(q.TicketId, ct);
}
