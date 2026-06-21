using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Domain.Repositories;

public interface ICorrectiveActionTicketRepository : IBaseRepository<CorrectiveActionTicket>
{
    Task<IEnumerable<CorrectiveActionTicket>> FindBySectorAsync(string sector, CancellationToken ct = default);
    Task<IEnumerable<CorrectiveActionTicket>> FindByStatusAsync(string status, CancellationToken ct = default);
    Task<IEnumerable<CorrectiveActionTicket>> FindByTechnicianAsync(string technicianId, CancellationToken ct = default);
}
