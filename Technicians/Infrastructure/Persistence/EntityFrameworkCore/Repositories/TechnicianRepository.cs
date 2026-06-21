using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Technicians.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TechnicianRepository(AppDbContext context)
    : BaseRepository<Technician>(context), ITechnicianRepository
{
}
