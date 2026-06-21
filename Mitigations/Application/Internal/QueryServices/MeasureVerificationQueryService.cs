using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;
using Acme.Center.Platform.Mitigations.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Application.Internal.QueryServices;

public class MeasureVerificationQueryService(IMeasureVerificationRepository repository) : IMeasureVerificationQueryService
{
    public async Task<IEnumerable<MeasureVerification>> Handle(GetAllMeasureVerificationsQuery q, CancellationToken ct) => await repository.ListAsync(ct);
    public async Task<MeasureVerification?> Handle(GetMeasureVerificationByIdQuery q, CancellationToken ct) => await repository.FindByIdAsync(q.Id, ct);
    public async Task<IEnumerable<MeasureVerification>> Handle(GetVerificationsByTicketQuery q, CancellationToken ct) => await repository.FindByTicketIdAsync(q.TicketId, ct);
    public async Task<IEnumerable<MeasureVerification>> Handle(GetVerificationsByVerdictQuery q, CancellationToken ct) => await repository.FindByVerdictAsync(q.Verdict, ct);
}
