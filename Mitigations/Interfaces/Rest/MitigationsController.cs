using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Acme.Center.Platform.Mitigations.Application.CommandServices;
using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest;

[ApiController]
[Route("api/v1/mitigations")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Mitigation Endpoints")]
public class MitigationsController(
    IMitigationCommandService commandService,
    IMitigationQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? riskAssessmentId, [FromQuery] string? ticketId, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(riskAssessmentId))
        {
            var items = await queryService.Handle(new GetMitigationsByAssessmentIdQuery(riskAssessmentId), ct);
            return Ok(items.Select(MitigationResourceFromEntityAssembler.ToResourceFromEntity));
        }
        if (!string.IsNullOrEmpty(ticketId))
        {
            var items = await queryService.Handle(new GetMitigationsByTicketIdQuery(ticketId), ct);
            return Ok(items.Select(MitigationResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var all = await queryService.Handle(new GetAllMitigationsQuery(), ct);
        return Ok(all.Select(MitigationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var item = await queryService.Handle(new GetMitigationByIdQuery(id), ct);
        if (item is null) return NotFound();
        return Ok(MitigationResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMitigationResource resource, CancellationToken ct)
    {
        var command = CreateMitigationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, ct);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = MitigationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateMitigationResource resource, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.Mitigation>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        existing.RiskAssessmentId = resource.RiskAssessmentId; existing.TicketId = resource.TicketId;
        existing.Code = resource.Code; existing.Description = resource.Description;
        existing.Responsible = resource.Responsible; existing.AssignedDate = resource.AssignedDate;
        existing.ExecutionDate = resource.ExecutionDate; existing.Status = resource.Status;
        existing.Result = resource.Result; existing.Observations = resource.Observations;
        await unitOfWork.CompleteAsync(ct);
        return Ok(MitigationResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.Mitigation>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.Mitigation>().Remove(existing);
        await unitOfWork.CompleteAsync(ct);
        return NoContent();
    }
}
