using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
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
[Route("api/v1/risk-assessments")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Risk Assessment Endpoints")]
public class RiskAssessmentsController(
    IRiskAssessmentCommandService commandService,
    IRiskAssessmentQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all risk assessments")]
    public async Task<IActionResult> GetAll([FromQuery] string? sector, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(sector))
        {
            var query = new GetRiskAssessmentsBySectorQuery(sector);
            var items = await queryService.Handle(query, cancellationToken);
            return Ok(items.Select(RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity));
        }
        var allQuery = new GetAllRiskAssessmentsQuery();
        var all = await queryService.Handle(allQuery, cancellationToken);
        return Ok(all.Select(RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetRiskAssessmentByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRiskAssessmentResource resource, CancellationToken cancellationToken)
    {
        var command = CreateRiskAssessmentCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { error = result.Error!.ToString(), message = result.Message });
        var created = RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, RiskAssessmentResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.RiskAssessment>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        existing.Code = resource.Code; existing.Sector = resource.Sector; existing.HazardType = resource.HazardType;
        existing.Description = resource.Description; existing.Probability = resource.Probability;
        existing.Severity = resource.Severity; existing.RiskLevel = resource.RiskLevel;
        existing.ControlMeasures = resource.ControlMeasures; existing.Status = resource.Status;
        existing.EvaluationDate = resource.EvaluationDate; existing.UserId = resource.UserId;
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(RiskAssessmentResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<Domain.Model.Aggregates.RiskAssessment>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<Domain.Model.Aggregates.RiskAssessment>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}
