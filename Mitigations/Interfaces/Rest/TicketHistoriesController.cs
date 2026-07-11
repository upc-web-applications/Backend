using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Acme.Center.Platform.Mitigations.Application.CommandServices;
using Acme.Center.Platform.Mitigations.Application.QueryServices;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Queries;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest;

[ApiController]
[Route("api/v1/ticket-histories")]
[Authorize(Policy = "SupervisorOrAdministrator")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Ticket History Endpoints")]
public class TicketHistoriesController(
    ITicketHistoryCommandService commandService,
    ITicketHistoryQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? ticketId, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(ticketId))
        {
            var items = await queryService.Handle(new GetHistoriesByTicketQuery(ticketId), ct);
            return Ok(items.Select(TicketHistoryResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var all = await queryService.Handle(new GetAllTicketHistoriesQuery(), ct);
        return Ok(all.Select(TicketHistoryResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var item = await queryService.Handle(new GetTicketHistoryByIdQuery(id), ct);
        if (item is null) return NotFound();
        return Ok(TicketHistoryResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketHistoryResource resource, CancellationToken ct)
    {
        var command = CreateTicketHistoryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, ct);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = TicketHistoryResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var existing = await context.Set<TicketHistory>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        context.Set<TicketHistory>().Remove(existing);
        await unitOfWork.CompleteAsync(ct);
        return NoContent();
    }
}
