using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Acme.Center.Platform.Technicians.Application.CommandServices;
using Acme.Center.Platform.Technicians.Application.QueryServices;
using Acme.Center.Platform.Technicians.Domain.Model.Queries;
using Acme.Center.Platform.Technicians.Interfaces.Rest.Resources;
using Acme.Center.Platform.Technicians.Interfaces.Rest.Transform;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.Technicians.Interfaces.Rest;

[ApiController]
[Route("api/v1/technicians")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Technician Endpoints")]
public class TechniciansController(
    ITechnicianCommandService commandService,
    ITechnicianQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all technicians")]
    [SwaggerResponse(200, "The technicians were found.", typeof(IEnumerable<TechnicianResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllTechniciansQuery();
        var technicians = await queryService.Handle(query, cancellationToken);
        var resources = technicians.Select(TechnicianResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get technician by id")]
    [SwaggerResponse(200, "The technician was found.", typeof(TechnicianResource))]
    [SwaggerResponse(404, "The technician was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetTechnicianByIdQuery(id);
        var technician = await queryService.Handle(query, cancellationToken);
        return TechnicianActionResultAssembler.ToActionResultFromGetByIdResult(
            this, technician, t => Ok(TechnicianResourceFromEntityAssembler.ToResourceFromEntity(t)));
    }

    [HttpPost]
    [SwaggerOperation("Create technician")]
    [SwaggerResponse(201, "The technician was created.", typeof(TechnicianResource))]
    [SwaggerResponse(400, "The technician was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateTechnicianResource resource, CancellationToken cancellationToken)
    {
        var command = CreateTechnicianCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        return TechnicianActionResultAssembler.ToActionResultFromCreateResult(
            this, result, created =>
            {
                var resource_ = TechnicianResourceFromEntityAssembler.ToResourceFromEntity(created);
                return CreatedAtAction(nameof(GetById), new { id = resource_.Id }, resource_);
            });
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update technician")]
    [SwaggerResponse(200, "The technician was updated.", typeof(TechnicianResource))]
    [SwaggerResponse(404, "The technician was not found.")]
    public async Task<IActionResult> Update(string id, [FromBody] TechnicianResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.Technician>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        existing.DocumentNumber = resource.DocumentNumber;
        existing.FullName = resource.FullName;
        existing.Specialty = resource.Specialty;
        existing.Phone = resource.Phone;
        existing.Email = resource.Email;
        existing.Status = resource.Status;
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(TechnicianResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete technician")]
    [SwaggerResponse(204, "The technician was deleted.")]
    [SwaggerResponse(404, "The technician was not found.")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.Technician>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.Technician>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}
