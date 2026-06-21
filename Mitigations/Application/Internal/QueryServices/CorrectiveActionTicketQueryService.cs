using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;
using Acme.Center.Platform.Mitigations.Domain.Repositories;

namespace Acme.Center.Platform.Mitigations.Application.Internal.QueryServices;

public class CorrectiveActionTicketQueryService(ICorrectiveActionTicketRepository repository) : ICorrectiveActionTicketQueryService
{
    public async Task<IEnumerable<CorrectiveActionTicket>> Handle(GetAllCorrectiveActionTicketsQuery q, CancellationToken ct) => await repository.ListAsync(ct);
    public async Task<CorrectiveActionTicket?> Handle(GetCorrectiveActionTicketByIdQuery q, CancellationToken ct) => await repository.FindByIdAsync(q.Id, ct);
    public async Task<IEnumerable<CorrectiveActionTicket>> Handle(GetTicketsBySectorQuery q, CancellationToken ct) => await repository.FindBySectorAsync(q.Sector, ct);
    public async Task<IEnumerable<CorrectiveActionTicket>> Handle(GetTicketsByStatusQuery q, CancellationToken ct) => await repository.FindByStatusAsync(q.Status, ct);
    public async Task<IEnumerable<CorrectiveActionTicket>> Handle(GetTicketsByTechnicianQuery q, CancellationToken ct) => await repository.FindByTechnicianAsync(q.AssignedTechnicianId, ct);
}
