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
[Route("api/v1/pattern-alerts")]
[Authorize(Policy = "SupervisorOnly")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Pattern Alert Endpoints")]
public class PatternAlertsController(
    IPatternAlertCommandService commandService,
    IPatternAlertQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all pattern alerts")]
    public async Task<IActionResult> GetAll([FromQuery] string? sector, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(sector))
        {
            var query = new GetPatternAlertsBySectorQuery(sector);
            var items = await queryService.Handle(query, cancellationToken);
            return Ok(items.Select(PatternAlertResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var allQuery = new GetAllPatternAlertsQuery();
        var all = await queryService.Handle(allQuery, cancellationToken);
        return Ok(all.Select(PatternAlertResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetPatternAlertByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(PatternAlertResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatternAlertResource resource, CancellationToken cancellationToken)
    {
        var command = CreatePatternAlertCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = PatternAlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, PatternAlertResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.PatternAlert>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        existing.PatternId = resource.PatternId; existing.SectorId = resource.SectorId;
        existing.Sector = resource.Sector; existing.RiskType = resource.RiskType;
        existing.OccurrenceCount = resource.OccurrenceCount; existing.FirstReportDate = resource.FirstReportDate;
        existing.Status = resource.Status; existing.GenerationDate = resource.GenerationDate;
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(PatternAlertResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.PatternAlert>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.PatternAlert>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}
