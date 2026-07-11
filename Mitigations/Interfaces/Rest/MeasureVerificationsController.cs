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
[Route("api/v1/measure-verifications")]
[Authorize(Policy = "SupervisorOrAdministrator")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Measure Verification Endpoints")]
public class MeasureVerificationsController(
    IMeasureVerificationCommandService commandService,
    IMeasureVerificationQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? ticketId, [FromQuery] string? verdict, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(ticketId))
        {
            var items = await queryService.Handle(new GetVerificationsByTicketQuery(ticketId), ct);
            return Ok(items.Select(MeasureVerificationResourceFromEntityAssembler.ToResourceFromEntity));
        }
        if (!string.IsNullOrEmpty(verdict))
        {
            var items = await queryService.Handle(new GetVerificationsByVerdictQuery(verdict), ct);
            return Ok(items.Select(MeasureVerificationResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var all = await queryService.Handle(new GetAllMeasureVerificationsQuery(), ct);
        return Ok(all.Select(MeasureVerificationResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var item = await queryService.Handle(new GetMeasureVerificationByIdQuery(id), ct);
        if (item is null) return NotFound();
        return Ok(MeasureVerificationResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMeasureVerificationResource resource, CancellationToken ct)
    {
        var command = CreateMeasureVerificationCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, ct);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = MeasureVerificationResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateMeasureVerificationResource resource, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.MeasureVerification>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        existing.TicketId = resource.TicketId; existing.SupervisorId = resource.SupervisorId;
        existing.SupervisorName = resource.SupervisorName; existing.Verdict = resource.Verdict;
        existing.JustificationComment = resource.JustificationComment; existing.VerificationDate = resource.VerificationDate;
        await unitOfWork.CompleteAsync(ct);
        return Ok(MeasureVerificationResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken ct)
    {
        var existing = await context.Set<Domain.Model.Aggregates.MeasureVerification>().FindAsync([id], ct);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.MeasureVerification>().Remove(existing);
        await unitOfWork.CompleteAsync(ct);
        return NoContent();
    }
}
