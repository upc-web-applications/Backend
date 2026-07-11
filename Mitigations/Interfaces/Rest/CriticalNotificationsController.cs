using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
[Route("api/v1/critical-notifications")]
[Authorize(Policy = "SupervisorOnly")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Critical Notification Endpoints")]
public class CriticalNotificationsController(
    ICriticalNotificationCommandService commandService,
    ICriticalNotificationQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? ticketId, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(ticketId))
        {
            var items = await queryService.Handle(new GetNotificationsByTicketQuery(ticketId), ct);
            return Ok(items.Select(CriticalNotificationResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var all = await queryService.Handle(new GetAllCriticalNotificationsQuery(), ct);
        return Ok(all.Select(CriticalNotificationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var item = await queryService.Handle(new GetCriticalNotificationByIdQuery(id), ct);
        if (item is null) return NotFound();
        return Ok(CriticalNotificationResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCriticalNotificationResource resource, CancellationToken ct)
    {
        var command = CreateCriticalNotificationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, ct);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = CriticalNotificationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateCriticalNotificationResource resource, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.CriticalNotification>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        existing.TicketId = resource.TicketId; existing.SupervisorId = resource.SupervisorId;
        existing.SupervisorName = resource.SupervisorName; existing.Message = resource.Message;
        existing.Sent = resource.Sent; existing.SentDate = resource.SentDate;
        await unitOfWork.CompleteAsync(ct);
        return Ok(CriticalNotificationResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.CriticalNotification>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.CriticalNotification>().Remove(existing);
        await unitOfWork.CompleteAsync(ct);
        return NoContent();
    }
}
