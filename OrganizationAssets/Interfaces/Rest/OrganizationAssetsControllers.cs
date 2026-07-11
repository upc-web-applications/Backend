using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Interfaces.Rest;

namespace Acme.Center.Platform.OrganizationAssets.Interfaces.Rest;

[Route("api/v1/headquarters")]
[Authorize(Policy = "OperatorOrSupervisor")]
public class HeadquartersController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Headquarters>(context, unitOfWork)
{
    [HttpPost]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Create([FromBody] Headquarters resource, CancellationToken cancellationToken)
        => base.Create(resource, cancellationToken);

    [HttpPut("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Update(string id, [FromBody] Headquarters resource, CancellationToken cancellationToken)
        => base.Update(id, resource, cancellationToken);

    [HttpPatch("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Patch(string id, [FromBody] Headquarters resource, CancellationToken cancellationToken)
        => base.Patch(id, resource, cancellationToken);

    [HttpDelete("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        => base.Delete(id, cancellationToken);
}

[Route("api/v1/areas")]
[Authorize(Policy = "OperatorOrSupervisor")]
public class AreasController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Area>(context, unitOfWork)
{
    [HttpGet("active")]
    public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
    {
        return Ok(await context.Areas.Where(area => area.Status == "Active").ToListAsync(cancellationToken));
    }

    [HttpPost]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Create([FromBody] Area resource, CancellationToken cancellationToken)
        => base.Create(resource, cancellationToken);

    [HttpPut("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Update(string id, [FromBody] Area resource, CancellationToken cancellationToken)
        => base.Update(id, resource, cancellationToken);

    [HttpPatch("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Patch(string id, [FromBody] Area resource, CancellationToken cancellationToken)
        => base.Patch(id, resource, cancellationToken);

    [HttpDelete("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        => base.Delete(id, cancellationToken);
}

[Route("api/v1/assets")]
[Authorize(Policy = "OperatorOrSupervisor")]
public class AssetsController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Asset>(context, unitOfWork)
{
    [HttpGet("by-area/{areaId:int}")]
    public async Task<IActionResult> GetByArea(int areaId, CancellationToken cancellationToken)
    {
        return Ok(await context.OrgAssets
            .Where(a => a.AreaId == areaId && a.Status == "Active")
            .ToListAsync(cancellationToken));
    }

    [HttpPost]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Create([FromBody] Asset resource, CancellationToken cancellationToken)
        => base.Create(resource, cancellationToken);

    [HttpPut("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Update(string id, [FromBody] Asset resource, CancellationToken cancellationToken)
        => base.Update(id, resource, cancellationToken);

    [HttpPatch("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Patch(string id, [FromBody] Asset resource, CancellationToken cancellationToken)
        => base.Patch(id, resource, cancellationToken);

    [HttpDelete("{id}")]
    [Authorize(Policy = "SupervisorOnly")]
    public override Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        => base.Delete(id, cancellationToken);
}
