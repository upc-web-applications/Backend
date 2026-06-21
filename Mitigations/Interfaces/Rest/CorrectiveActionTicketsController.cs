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
[Route("api/v1/corrective-action-tickets")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Corrective Action Ticket Endpoints")]
public class CorrectiveActionTicketsController(
    ICorrectiveActionTicketCommandService commandService,
    ICorrectiveActionTicketQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? sectorId, [FromQuery] string? status, [FromQuery] string? assignedTechnicianId, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(sectorId))
        {
            var items = await queryService.Handle(new GetTicketsBySectorQuery(sectorId), ct);
            return Ok(items.Select(CorrectiveActionTicketResourceFromEntityAssembler.ToResourceFromEntity));
        }
        if (!string.IsNullOrEmpty(status))
        {
            var items = await queryService.Handle(new GetTicketsByStatusQuery(status), ct);
            return Ok(items.Select(CorrectiveActionTicketResourceFromEntityAssembler.ToResourceFromEntity));
        }
        if (!string.IsNullOrEmpty(assignedTechnicianId))
        {
            var items = await queryService.Handle(new GetTicketsByTechnicianQuery(assignedTechnicianId), ct);
            return Ok(items.Select(CorrectiveActionTicketResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var all = await queryService.Handle(new GetAllCorrectiveActionTicketsQuery(), ct);
        return Ok(all.Select(CorrectiveActionTicketResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var item = await queryService.Handle(new GetCorrectiveActionTicketByIdQuery(id), ct);
        if (item is null) return NotFound();
        return Ok(CorrectiveActionTicketResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCorrectiveActionTicketResource resource, CancellationToken ct)
    {
        var command = CreateCorrectiveActionTicketCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, ct);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = CorrectiveActionTicketResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateCorrectiveActionTicketResource resource, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.CorrectiveActionTicket>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        existing.TicketNumber = resource.TicketNumber; existing.ReportId = resource.ReportId;
        existing.SectorId = resource.SectorId; existing.Sector = resource.Sector;
        existing.RiskType = resource.RiskType; existing.CriticalityLevel = resource.CriticalityLevel;
        existing.Status = resource.Status; existing.Instructions = resource.Instructions;
        existing.AssignedTechnicianId = resource.AssignedTechnicianId; existing.TechnicianName = resource.TechnicianName;
        existing.CreatedDate = resource.CreatedDate; existing.ClosureDate = resource.ClosureDate;
        existing.SlaLimitHours = resource.SlaLimitHours; existing.SlaMissed = resource.SlaMissed;
        await unitOfWork.CompleteAsync(ct);
        return Ok(CorrectiveActionTicketResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.CorrectiveActionTicket>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.CorrectiveActionTicket>().Remove(existing);
        await unitOfWork.CompleteAsync(ct);
        return NoContent();
    }
}
