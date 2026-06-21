using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Hazards.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class HazardRepository(AppDbContext context)
    : BaseRepository<Hazard>(context), IHazardRepository
{
}
