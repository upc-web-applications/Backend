using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;
using Acme.Center.Platform.Mitigations.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Application.Internal.QueryServices;

public class MitigationQueryService(IMitigationRepository repository) : IMitigationQueryService
{
    public async Task<IEnumerable<Mitigation>> Handle(GetAllMitigationsQuery q, CancellationToken ct) => await repository.ListAsync(ct);
    public async Task<Mitigation?> Handle(GetMitigationByIdQuery q, CancellationToken ct) => await repository.FindByIdAsync(q.Id, ct);
    public async Task<IEnumerable<Mitigation>> Handle(GetMitigationsByAssessmentIdQuery q, CancellationToken ct) => await repository.FindByAssessmentIdAsync(q.RiskAssessmentId, ct);
    public async Task<IEnumerable<Mitigation>> Handle(GetMitigationsByTicketIdQuery q, CancellationToken ct) => await repository.FindByTicketIdAsync(q.TicketId, ct);
}
