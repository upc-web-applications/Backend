using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Acme.Center.Platform.RiskAssessments.Application.CommandServices;
using Acme.Center.Platform.RiskAssessments.Application.QueryServices;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest;

[ApiController]
[Route("api/v1/area-criticality-levels")]
[Authorize(Policy = "SupervisorOnly")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Area Criticality Level Endpoints")]
public class AreaCriticalityLevelsController(
    IAreaCriticalityLevelCommandService commandService,
    IAreaCriticalityLevelQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all area criticality levels")]
    public async Task<IActionResult> GetAll([FromQuery] string? sector, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(sector))
        {
            var query = new GetAreaCriticalityLevelsBySectorQuery(sector);
            var items = await queryService.Handle(query, cancellationToken);
            return Ok(items.Select(AreaCriticalityLevelResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var allQuery = new GetAllAreaCriticalityLevelsQuery();
        var all = await queryService.Handle(allQuery, cancellationToken);
        return Ok(all.Select(AreaCriticalityLevelResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetAreaCriticalityLevelByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(AreaCriticalityLevelResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAreaCriticalityLevelResource resource, CancellationToken cancellationToken)
    {
        var command = CreateAreaCriticalityLevelCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = AreaCriticalityLevelResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, AreaCriticalityLevelResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.AreaCriticalityLevel>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        existing.SectorId = resource.SectorId; existing.Sector = resource.Sector;
        existing.CriticalityLevel = resource.CriticalityLevel; existing.MapIntensity = resource.MapIntensity;
        existing.LastUpdated = resource.LastUpdated;
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(AreaCriticalityLevelResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.AreaCriticalityLevel>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.AreaCriticalityLevel>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}
