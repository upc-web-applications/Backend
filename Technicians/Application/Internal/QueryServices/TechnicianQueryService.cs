using Acme.Center.Platform.Technicians.Application.QueryServices;
using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Domain.Model.Queries;
using Acme.Center.Platform.Technicians.Domain.Repositories;

namespace Acme.Center.Platform.Technicians.Application.Internal.QueryServices;

public class TechnicianQueryService(ITechnicianRepository technicianRepository) : ITechnicianQueryService
{
    public async Task<IEnumerable<Technician>> Handle(GetAllTechniciansQuery query, CancellationToken cancellationToken)
    {
        return await technicianRepository.ListAsync(cancellationToken);
    }

    public async Task<Technician?> Handle(GetTechnicianByIdQuery query, CancellationToken cancellationToken)
    {
        return await technicianRepository.FindByIdAsync(query.Id, cancellationToken);
    }
}
