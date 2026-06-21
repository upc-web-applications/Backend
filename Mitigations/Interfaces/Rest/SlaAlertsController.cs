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
[Route("api/v1/sla-alerts")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("SLA Alert Endpoints")]
public class SlaAlertsController(
    ISlaAlertCommandService commandService,
    ISlaAlertQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? ticketId, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(ticketId))
        {
            var items = await queryService.Handle(new GetAlertsByTicketQuery(ticketId), ct);
            return Ok(items.Select(SlaAlertResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var all = await queryService.Handle(new GetAllSlaAlertsQuery(), ct);
        return Ok(all.Select(SlaAlertResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var item = await queryService.Handle(new GetSlaAlertByIdQuery(id), ct);
        if (item is null) return NotFound();
        return Ok(SlaAlertResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSlaAlertResource resource, CancellationToken ct)
    {
        var command = CreateSlaAlertCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, ct);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = SlaAlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateSlaAlertResource resource, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.SlaAlert>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        existing.TicketId = resource.TicketId; existing.ElapsedHours = resource.ElapsedHours;
        existing.SlaLimitHours = resource.SlaLimitHours; existing.AlertDate = resource.AlertDate;
        existing.NotifiedTo = resource.NotifiedTo; existing.NotifiedName = resource.NotifiedName;
        await unitOfWork.CompleteAsync(ct);
        return Ok(SlaAlertResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.SlaAlert>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.SlaAlert>().Remove(existing);
        await unitOfWork.CompleteAsync(ct);
        return NoContent();
    }
}
