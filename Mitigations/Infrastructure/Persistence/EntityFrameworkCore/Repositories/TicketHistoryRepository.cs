using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TicketHistoryRepository(AppDbContext context) : BaseRepository<TicketHistory>(context), ITicketHistoryRepository
{
    public async Task<IEnumerable<TicketHistory>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default)
        => await Context.Set<TicketHistory>().Where(h => h.TicketId == ticketId).ToListAsync(ct);
}
