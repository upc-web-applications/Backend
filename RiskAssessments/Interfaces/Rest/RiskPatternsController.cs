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
[Route("api/v1/risk-patterns")]
[Authorize(Policy = "SupervisorOnly")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Risk Pattern Endpoints")]
public class RiskPatternsController(
    IRiskPatternCommandService commandService,
    IRiskPatternQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all risk patterns")]
    public async Task<IActionResult> GetAll([FromQuery] string? sector, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(sector))
        {
            var query = new GetRiskPatternsBySectorQuery(sector);
            var items = await queryService.Handle(query, cancellationToken);
            return Ok(items.Select(RiskPatternResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var allQuery = new GetAllRiskPatternsQuery();
        var all = await queryService.Handle(allQuery, cancellationToken);
        return Ok(all.Select(RiskPatternResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetRiskPatternByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(RiskPatternResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRiskPatternResource resource, CancellationToken cancellationToken)
    {
        var command = CreateRiskPatternCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = RiskPatternResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, RiskPatternResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.RiskPattern>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        existing.SectorId = resource.SectorId; existing.Sector = resource.Sector;
        existing.IncidentType = resource.IncidentType; existing.HazardType = resource.HazardType;
        existing.Description = resource.Description; existing.Frequency = resource.Frequency;
        existing.FirstOccurrenceDate = resource.FirstOccurrenceDate; existing.AnalysisPeriodDays = resource.AnalysisPeriodDays;
        existing.IsReviewed = resource.IsReviewed; existing.ReviewDate = resource.ReviewDate; existing.ReviewedBy = resource.ReviewedBy;
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(RiskPatternResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.RiskPattern>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.RiskPattern>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}
