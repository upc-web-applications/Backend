using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Interfaces.Rest;

namespace Acme.Center.Platform.OrganizationAssets.Interfaces.Rest;

[Route("api/v1/headquarters")]
public class HeadquartersController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Headquarters>(context, unitOfWork);

[Route("api/v1/areas")]
public class AreasController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Area>(context, unitOfWork)
{
    [HttpGet("active")]
    public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
    {
        return Ok(await context.Areas.Where(area => area.Status == "Active").ToListAsync(cancellationToken));
    }
}

[Route("api/v1/assets")]
public class AssetsController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Asset>(context, unitOfWork)
{
    [HttpGet("by-area/{areaId:int}")]
    public async Task<IActionResult> GetByArea(int areaId, CancellationToken cancellationToken)
    {
        return Ok(await context.OrgAssets
            .Where(a => a.AreaId == areaId && a.Status == "Active")
            .ToListAsync(cancellationToken));
    }
}
