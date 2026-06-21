using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SlaAlertRepository(AppDbContext context) : BaseRepository<SlaAlert>(context), ISlaAlertRepository
{
    public async Task<IEnumerable<SlaAlert>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default)
        => await Context.Set<SlaAlert>().Where(a => a.TicketId == ticketId).ToListAsync(ct);
}
