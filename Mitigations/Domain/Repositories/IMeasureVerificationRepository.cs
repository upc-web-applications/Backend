using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Domain.Repositories;

public interface IMeasureVerificationRepository : IBaseRepository<MeasureVerification>
{
    Task<IEnumerable<MeasureVerification>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default);
    Task<IEnumerable<MeasureVerification>> FindByVerdictAsync(string verdict, CancellationToken ct = default);
}
