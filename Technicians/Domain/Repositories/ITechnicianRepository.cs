using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.Technicians.Domain.Repositories;

public interface ITechnicianRepository : IBaseRepository<Technician>
{
}
