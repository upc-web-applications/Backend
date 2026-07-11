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
[Route("api/v1/daily-summaries")]
[Authorize(Policy = "SupervisorOnly")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Daily Summary Endpoints")]
public class DailySummariesController(
    IDailySummaryCommandService commandService,
    IDailySummaryQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all daily summaries")]
    public async Task<IActionResult> GetAll([FromQuery] string? sector, [FromQuery] DateTime? date, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(sector))
        {
            var query = new GetDailySummariesBySectorQuery(sector);
            var items = await queryService.Handle(query, cancellationToken);
            return Ok(items.Select(DailySummaryResourceFromEntityAssembler.ToResourceFromEntity));
        }
        if (date.HasValue)
        {
            var query = new GetDailySummariesByDateQuery(date.Value);
            var items = await queryService.Handle(query, cancellationToken);
            return Ok(items.Select(DailySummaryResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var allQuery = new GetAllDailySummariesQuery();
        var all = await queryService.Handle(allQuery, cancellationToken);
        return Ok(all.Select(DailySummaryResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetDailySummaryByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(DailySummaryResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDailySummaryResource resource, CancellationToken cancellationToken)
    {
        var command = CreateDailySummaryCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = DailySummaryResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, DailySummaryResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.DailySummary>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        existing.Date = resource.Date; existing.SectorId = resource.SectorId;
        existing.Sector = resource.Sector; existing.TotalNew = resource.TotalNew;
        existing.TotalInProgress = resource.TotalInProgress; existing.TotalResolved = resource.TotalResolved;
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(DailySummaryResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.DailySummary>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.DailySummary>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}
