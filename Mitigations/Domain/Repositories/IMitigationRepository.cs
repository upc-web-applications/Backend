using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Domain.Repositories;

public interface IMitigationRepository : IBaseRepository<Mitigation>
{
    Task<IEnumerable<Mitigation>> FindByAssessmentIdAsync(string assessmentId, CancellationToken ct = default);
    Task<IEnumerable<Mitigation>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default);
}
