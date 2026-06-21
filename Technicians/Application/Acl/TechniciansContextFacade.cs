using Acme.Center.Platform.Technicians.Application.QueryServices;
using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Domain.Model.Queries;
using Acme.Center.Platform.Technicians.Interfaces.Acl;

namespace Acme.Center.Platform.Technicians.Application.Acl;

public class TechniciansContextFacade(ITechnicianQueryService queryService) : ITechniciansContextFacade
{
    public async Task<Technician?> FetchTechnicianById(string technicianId, CancellationToken cancellationToken = default)
    {
        var query = new GetTechnicianByIdQuery(technicianId);
        return await queryService.Handle(query, cancellationToken);
    }

    public async Task<IEnumerable<Technician>> FetchAllTechnicians(CancellationToken cancellationToken = default)
    {
        var query = new GetAllTechniciansQuery();
        return await queryService.Handle(query, cancellationToken);
    }
}
