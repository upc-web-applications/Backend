using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Acme.Center.Platform.Hazards.Application.CommandServices;
using Acme.Center.Platform.Hazards.Application.QueryServices;
using Acme.Center.Platform.Hazards.Domain.Model.Queries;
using Acme.Center.Platform.Hazards.Interfaces.Rest.Resources;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Interfaces.Rest.Transform;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.Hazards.Interfaces.Rest;

[ApiController]
[Route("api/v1/hazards")]
[Authorize(Policy = "SupervisorOnly")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Hazard Endpoints")]
public class HazardsController(
    IHazardCommandService commandService,
    IHazardQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all hazards")]
    [SwaggerResponse(200, "The hazards were found.", typeof(IEnumerable<HazardResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllHazardsQuery();
        var hazards = await queryService.Handle(query, cancellationToken);
        var resources = hazards.Select(HazardResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get hazard by id")]
    [SwaggerResponse(200, "The hazard was found.", typeof(HazardResource))]
    [SwaggerResponse(404, "The hazard was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetHazardByIdQuery(id);
        var hazard = await queryService.Handle(query, cancellationToken);
        return HazardActionResultAssembler.ToActionResultFromGetByIdResult(
            this, hazard, h => Ok(HazardResourceFromEntityAssembler.ToResourceFromEntity(h)));
    }

    [HttpPost]
    [SwaggerOperation("Create hazard")]
    [SwaggerResponse(201, "The hazard was created.", typeof(HazardResource))]
    [SwaggerResponse(400, "The hazard was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateHazardResource resource, CancellationToken cancellationToken)
    {
        var command = CreateHazardCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return HazardActionResultAssembler.ToActionResultFromCreateResult(
            this, result, created =>
            {
                var resource_ = HazardResourceFromEntityAssembler.ToResourceFromEntity(created);
                return CreatedAtAction(nameof(GetById), new { id = resource_.Id }, resource_);
            });
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update hazard")]
    [SwaggerResponse(200, "The hazard was updated.", typeof(HazardResource))]
    [SwaggerResponse(404, "The hazard was not found.")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateHazardResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.Hazard>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        var entity = UpdateHazardCommandFromResourceAssembler.ToEntityFromResource(id, resource);
        context.Entry(existing).CurrentValues.SetValues(entity);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(HazardResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete hazard")]
    [SwaggerResponse(204, "The hazard was deleted.")]
    [SwaggerResponse(404, "The hazard was not found.")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.Hazard>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.Hazard>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}
