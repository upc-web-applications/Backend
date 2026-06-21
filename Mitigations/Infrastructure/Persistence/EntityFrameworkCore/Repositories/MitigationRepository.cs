using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MitigationRepository(AppDbContext context) : BaseRepository<Mitigation>(context), IMitigationRepository
{
    public async Task<IEnumerable<Mitigation>> FindByAssessmentIdAsync(string assessmentId, CancellationToken ct = default)
        => await Context.Set<Mitigation>().Where(m => m.RiskAssessmentId == assessmentId).ToListAsync(ct);
    public async Task<IEnumerable<Mitigation>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default)
        => await Context.Set<Mitigation>().Where(m => m.TicketId == ticketId).ToListAsync(ct);
}
