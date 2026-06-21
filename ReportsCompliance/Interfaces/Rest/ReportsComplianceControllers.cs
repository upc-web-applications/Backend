using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.ReportsCompliance.Application.CommandServices;
using Acme.Center.Platform.ReportsCompliance.Application.QueryServices;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Queries;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest;

// ── MonthlyReports: GET all, GET id, GET year, POST, PUT ──

[ApiController]
[Route("api/v1/monthly_reports")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Monthly Reports Endpoints")]
public class MonthlyReportsController(
    IReportsComplianceCommandService commandService,
    IReportsComplianceQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all monthly reports")]
    [SwaggerResponse(200, "The monthly reports were found.", typeof(IEnumerable<MonthlyReportResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllMonthlyReportsQuery();
        var reports = await queryService.Handle(query, cancellationToken);
        var resources = reports.Select(MonthlyReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get monthly report by id")]
    [SwaggerResponse(200, "The monthly report was found.", typeof(MonthlyReportResource))]
    [SwaggerResponse(404, "The monthly report was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetMonthlyReportByIdQuery(id);
        var report = await queryService.Handle(query, cancellationToken);
        if (report is null) return NotFound();
        return Ok(MonthlyReportResourceFromEntityAssembler.ToResourceFromEntity(report));
    }

    [HttpGet("year/{year:int}")]
    [SwaggerOperation("Get monthly reports by year")]
    [SwaggerResponse(200, "The monthly reports were found.", typeof(IEnumerable<MonthlyReportResource>))]
    public async Task<IActionResult> GetByYear(int year, CancellationToken cancellationToken)
    {
        var query = new GetMonthlyReportsByYearQuery(year);
        var reports = await queryService.Handle(query, cancellationToken);
        var resources = reports.Select(MonthlyReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation("Create monthly report")]
    [SwaggerResponse(201, "The monthly report was created.", typeof(MonthlyReportResource))]
    [SwaggerResponse(400, "The monthly report was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateMonthlyReportResource resource, CancellationToken cancellationToken)
    {
        var command = CreateMonthlyReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = MonthlyReportResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update monthly report")]
    [SwaggerResponse(200, "The monthly report was updated.", typeof(MonthlyReportResource))]
    [SwaggerResponse(404, "The monthly report was not found.")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateMonthlyReportResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<MonthlyReport>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        var entity = UpdateMonthlyReportCommandFromResourceAssembler.ToEntityFromResource(id, resource);
        context.Entry(existing).CurrentValues.SetValues(entity);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(MonthlyReportResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }
}

// ── CumulativeStIndicators: GET all, GET id ──

[ApiController]
[Route("api/v1/cumulative_st_indicators")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Cumulative ST Indicators Endpoints")]
public class CumulativeStIndicatorsController(
    IReportsComplianceQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all cumulative ST indicators")]
    [SwaggerResponse(200, "The indicators were found.", typeof(IEnumerable<CumulativeStIndicatorResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCumulativeStIndicatorsQuery();
        var items = await queryService.Handle(query, cancellationToken);
        var resources = items.Select(CumulativeStIndicatorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get cumulative ST indicator by id")]
    [SwaggerResponse(200, "The indicator was found.", typeof(CumulativeStIndicatorResource))]
    [SwaggerResponse(404, "The indicator was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetCumulativeStIndicatorByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(CumulativeStIndicatorResourceFromEntityAssembler.ToResourceFromEntity(item));
    }
}

// ── HistoricalIncidentRecords: GET all, GET id, POST, PUT ──

[ApiController]
[Route("api/v1/historical_incident_records")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Historical Incident Records Endpoints")]
public class HistoricalIncidentRecordsController(
    IReportsComplianceCommandService commandService,
    IReportsComplianceQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all historical incident records")]
    [SwaggerResponse(200, "The records were found.", typeof(IEnumerable<HistoricalIncidentRecordResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllHistoricalIncidentRecordsQuery();
        var items = await queryService.Handle(query, cancellationToken);
        var resources = items.Select(HistoricalIncidentRecordResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get historical incident record by id")]
    [SwaggerResponse(200, "The record was found.", typeof(HistoricalIncidentRecordResource))]
    [SwaggerResponse(404, "The record was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetHistoricalIncidentRecordByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(HistoricalIncidentRecordResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    [SwaggerOperation("Create historical incident record")]
    [SwaggerResponse(201, "The record was created.", typeof(HistoricalIncidentRecordResource))]
    [SwaggerResponse(400, "The record was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateHistoricalIncidentRecordResource resource, CancellationToken cancellationToken)
    {
        var command = CreateHistoricalIncidentRecordCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = HistoricalIncidentRecordResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update historical incident record")]
    [SwaggerResponse(200, "The record was updated.", typeof(HistoricalIncidentRecordResource))]
    [SwaggerResponse(404, "The record was not found.")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateHistoricalIncidentRecordResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<HistoricalIncidentRecord>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        var entity = UpdateHistoricalIncidentRecordCommandFromResourceAssembler.ToEntityFromResource(id, resource);
        context.Entry(existing).CurrentValues.SetValues(entity);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(HistoricalIncidentRecordResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }
}

// ── AnnualOhsPlan: GET all, GET id, PUT ──

[ApiController]
[Route("api/v1/annual_ohs_plan")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Annual OHS Plan Endpoints")]
public class AnnualOhsPlanController(
    IReportsComplianceQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all annual OHS plans")]
    [SwaggerResponse(200, "The plans were found.", typeof(IEnumerable<AnnualOhsPlanResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllAnnualOhsPlansQuery();
        var items = await queryService.Handle(query, cancellationToken);
        var resources = items.Select(AnnualOhsPlanResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get annual OHS plan by id")]
    [SwaggerResponse(200, "The plan was found.", typeof(AnnualOhsPlanResource))]
    [SwaggerResponse(404, "The plan was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetAnnualOhsPlanByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(AnnualOhsPlanResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update annual OHS plan")]
    [SwaggerResponse(200, "The plan was updated.", typeof(AnnualOhsPlanResource))]
    [SwaggerResponse(404, "The plan was not found.")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateAnnualOhsPlanResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<AnnualOhsPlan>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        var entity = UpdateAnnualOhsPlanCommandFromResourceAssembler.ToEntityFromResource(id, resource);
        context.Entry(existing).CurrentValues.SetValues(entity);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(AnnualOhsPlanResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }
}

// ── PredictiveIndicators: GET all, GET id ──

[ApiController]
[Route("api/v1/predictive_indicators")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Predictive Indicators Endpoints")]
public class PredictiveIndicatorsController(
    IReportsComplianceQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all predictive indicators")]
    [SwaggerResponse(200, "The indicators were found.", typeof(IEnumerable<PredictiveIndicatorResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllPredictiveIndicatorsQuery();
        var items = await queryService.Handle(query, cancellationToken);
        var resources = items.Select(PredictiveIndicatorResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get predictive indicator by id")]
    [SwaggerResponse(200, "The indicator was found.", typeof(PredictiveIndicatorResource))]
    [SwaggerResponse(404, "The indicator was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetPredictiveIndicatorByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(PredictiveIndicatorResourceFromEntityAssembler.ToResourceFromEntity(item));
    }
}

// ── CriticalAlerts: GET all, GET id, PUT, DELETE ──

[ApiController]
[Route("api/v1/critical_alerts")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Critical Alerts Endpoints")]
public class CriticalAlertsController(
    IReportsComplianceQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all critical alerts")]
    [SwaggerResponse(200, "The alerts were found.", typeof(IEnumerable<CriticalAlertResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllCriticalAlertsQuery();
        var items = await queryService.Handle(query, cancellationToken);
        var resources = items.Select(CriticalAlertResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get critical alert by id")]
    [SwaggerResponse(200, "The alert was found.", typeof(CriticalAlertResource))]
    [SwaggerResponse(404, "The alert was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetCriticalAlertByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(CriticalAlertResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPut("{id}")]
    [SwaggerOperation("Update critical alert")]
    [SwaggerResponse(200, "The alert was updated.", typeof(CriticalAlertResource))]
    [SwaggerResponse(404, "The alert was not found.")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateCriticalAlertResource resource, CancellationToken cancellationToken)
    {
        var existing = await context.Set<CriticalAlert>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        var entity = UpdateCriticalAlertCommandFromResourceAssembler.ToEntityFromResource(id, resource);
        context.Entry(existing).CurrentValues.SetValues(entity);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(CriticalAlertResourceFromEntityAssembler.ToResourceFromEntity(existing));
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete critical alert")]
    [SwaggerResponse(204, "The alert was deleted.")]
    [SwaggerResponse(404, "The alert was not found.")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<CriticalAlert>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<CriticalAlert>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}

// ── GeneratedReports: GET all, GET id, POST, DELETE ──

[ApiController]
[Route("api/v1/generated_reports")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Generated Reports Endpoints")]
public class GeneratedReportsController(
    IReportsComplianceCommandService commandService,
    IReportsComplianceQueryService queryService,
    AppDbContext context,
    IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all generated reports")]
    [SwaggerResponse(200, "The reports were found.", typeof(IEnumerable<GeneratedReportResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllGeneratedReportsQuery();
        var items = await queryService.Handle(query, cancellationToken);
        var resources = items.Select(GeneratedReportResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get generated report by id")]
    [SwaggerResponse(200, "The report was found.", typeof(GeneratedReportResource))]
    [SwaggerResponse(404, "The report was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetGeneratedReportByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(GeneratedReportResourceFromEntityAssembler.ToResourceFromEntity(item));
    }

    [HttpPost]
    [SwaggerOperation("Create generated report")]
    [SwaggerResponse(201, "The report was created.", typeof(GeneratedReportResource))]
    [SwaggerResponse(400, "The report was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateGeneratedReportResource resource, CancellationToken cancellationToken)
    {
        var command = CreateGeneratedReportCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = GeneratedReportResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete generated report")]
    [SwaggerResponse(204, "The report was deleted.")]
    [SwaggerResponse(404, "The report was not found.")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        var existing = await context.Set<GeneratedReport>().FindAsync([id], cancellationToken);
        if (existing is null) return NotFound();
        context.Set<GeneratedReport>().Remove(existing);
        await unitOfWork.CompleteAsync(cancellationToken);
        return NoContent();
    }
}

// ── KpiDashboard: GET all, GET id ──

[ApiController]
[Route("api/v1/kpi_dashboard")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("KPI Dashboard Endpoints")]
public class KpiDashboardController(
    IReportsComplianceQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all KPI dashboard entries")]
    [SwaggerResponse(200, "The KPI entries were found.", typeof(IEnumerable<KpiDashboardResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllKpiDashboardQuery();
        var items = await queryService.Handle(query, cancellationToken);
        var resources = items.Select(KpiDashboardResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get KPI dashboard entry by id")]
    [SwaggerResponse(200, "The KPI entry was found.", typeof(KpiDashboardResource))]
    [SwaggerResponse(404, "The KPI entry was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetKpiDashboardByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(KpiDashboardResourceFromEntityAssembler.ToResourceFromEntity(item));
    }
}

// ── HistoricalTrends: GET all, GET id ──

[ApiController]
[Route("api/v1/historical_trends")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Historical Trends Endpoints")]
public class HistoricalTrendsController(
    IReportsComplianceQueryService queryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all historical trends")]
    [SwaggerResponse(200, "The trends were found.", typeof(IEnumerable<HistoricalTrendResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllHistoricalTrendsQuery();
        var items = await queryService.Handle(query, cancellationToken);
        var resources = items.Select(HistoricalTrendResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get historical trend by id")]
    [SwaggerResponse(200, "The trend was found.", typeof(HistoricalTrendResource))]
    [SwaggerResponse(404, "The trend was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetHistoricalTrendByIdQuery(id);
        var item = await queryService.Handle(query, cancellationToken);
        if (item is null) return NotFound();
        return Ok(HistoricalTrendResourceFromEntityAssembler.ToResourceFromEntity(item));
    }
}
