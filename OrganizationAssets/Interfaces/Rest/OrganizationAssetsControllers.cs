using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.OrganizationAssets.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Interfaces.Rest;

namespace RiskGuard.Platform.OrganizationAssets.Interfaces.Rest;

[Route("api/v1/sedes")]
public class SedesController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Sede>(context, unitOfWork);

[Route("api/v1/areas")]
public class AreasController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Area>(context, unitOfWork)
{
    [HttpGet("active")]
    public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
    {
        return Ok(await context.Areas.Where(area => area.Estado == "Activo").ToListAsync(cancellationToken));
    }
}

[Route("api/v1/activos")]
public class ActivosController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Activo>(context, unitOfWork)
{
    [HttpGet("by-area/{areaId:int}")]
    public async Task<IActionResult> GetByArea(int areaId, CancellationToken cancellationToken)
    {
        return Ok(await context.Activos
            .Where(asset => asset.AreaId == areaId && asset.Estado == "Activo")
            .ToListAsync(cancellationToken));
    }
}
