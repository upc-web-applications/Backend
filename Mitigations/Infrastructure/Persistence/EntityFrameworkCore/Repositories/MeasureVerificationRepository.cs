using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MeasureVerificationRepository(AppDbContext context) : BaseRepository<MeasureVerification>(context), IMeasureVerificationRepository
{
    public async Task<IEnumerable<MeasureVerification>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default)
        => await Context.Set<MeasureVerification>().Where(v => v.TicketId == ticketId).ToListAsync(ct);
    public async Task<IEnumerable<MeasureVerification>> FindByVerdictAsync(string verdict, CancellationToken ct = default)
        => await Context.Set<MeasureVerification>().Where(v => v.Verdict == verdict).ToListAsync(ct);
}
