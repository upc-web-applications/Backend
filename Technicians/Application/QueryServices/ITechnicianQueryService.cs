using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Domain.Model.Queries;

namespace Acme.Center.Platform.Technicians.Application.QueryServices;

public interface ITechnicianQueryService
{
    Task<IEnumerable<Technician>> Handle(GetAllTechniciansQuery query, CancellationToken cancellationToken);
    Task<Technician?> Handle(GetTechnicianByIdQuery query, CancellationToken cancellationToken);
}
