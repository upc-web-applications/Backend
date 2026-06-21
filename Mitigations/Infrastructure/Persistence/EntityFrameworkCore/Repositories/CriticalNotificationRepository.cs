using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CriticalNotificationRepository(AppDbContext context) : BaseRepository<CriticalNotification>(context), ICriticalNotificationRepository
{
    public async Task<IEnumerable<CriticalNotification>> FindByTicketIdAsync(string ticketId, CancellationToken ct = default)
        => await Context.Set<CriticalNotification>().Where(n => n.TicketId == ticketId).ToListAsync(ct);
}
