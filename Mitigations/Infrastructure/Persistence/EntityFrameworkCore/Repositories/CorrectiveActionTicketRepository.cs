using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CorrectiveActionTicketRepository(AppDbContext context) : BaseRepository<CorrectiveActionTicket>(context), ICorrectiveActionTicketRepository
{
    public async Task<IEnumerable<CorrectiveActionTicket>> FindBySectorAsync(string sector, CancellationToken ct = default)
        => await Context.Set<CorrectiveActionTicket>().Where(t => t.Sector == sector).ToListAsync(ct);
    public async Task<IEnumerable<CorrectiveActionTicket>> FindByStatusAsync(string status, CancellationToken ct = default)
        => await Context.Set<CorrectiveActionTicket>().Where(t => t.Status == status).ToListAsync(ct);
    public async Task<IEnumerable<CorrectiveActionTicket>> FindByTechnicianAsync(string technicianId, CancellationToken ct = default)
        => await Context.Set<CorrectiveActionTicket>().Where(t => t.AssignedTechnicianId == technicianId).ToListAsync(ct);
}
