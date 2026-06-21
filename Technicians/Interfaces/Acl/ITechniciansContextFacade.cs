using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;

namespace Acme.Center.Platform.Technicians.Interfaces.Acl;

public interface ITechniciansContextFacade
{
    Task<Technician?> FetchTechnicianById(string technicianId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Technician>> FetchAllTechnicians(CancellationToken cancellationToken = default);
}
