using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RiskGuard.Platform.ReportsCompliance.Application.CommandServices;
using RiskGuard.Platform.ReportsCompliance.Application.QueryServices;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Queries;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;
using RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest;

[ApiController]
[Route("api/v1/monthly_reports")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Monthly Reports Endpoints")]
public class MonthlyReportsController(
    IReportsComplianceCommandService commandService,
    IReportsComplianceQueryService queryService) : ControllerBase
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
}

[ApiController]
[Route("api/v1/cumulative_st_indicators")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Cumulative ST Indicators Endpoints")]
public class CumulativeStIndicatorsController(
    IReportsComplianceCommandService commandService,
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

    [HttpPost]
    [SwaggerOperation("Create cumulative ST indicator")]
    [SwaggerResponse(201, "The indicator was created.", typeof(CumulativeStIndicatorResource))]
    [SwaggerResponse(400, "The indicator was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateCumulativeStIndicatorResource resource, CancellationToken cancellationToken)
    {
        var command = CreateCumulativeStIndicatorCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = CumulativeStIndicatorResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(null, created);
    }
}

[ApiController]
[Route("api/v1/historical_incident_records")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Historical Incident Records Endpoints")]
public class HistoricalIncidentRecordsController(
    IReportsComplianceCommandService commandService,
    IReportsComplianceQueryService queryService) : ControllerBase
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
        return CreatedAtAction(null, created);
    }
}

[ApiController]
[Route("api/v1/annual_ohs_plan")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Annual OHS Plan Endpoints")]
public class AnnualOhsPlanController(
    IReportsComplianceCommandService commandService,
    IReportsComplianceQueryService queryService) : ControllerBase
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

    [HttpPost]
    [SwaggerOperation("Create annual OHS plan")]
    [SwaggerResponse(201, "The plan was created.", typeof(AnnualOhsPlanResource))]
    [SwaggerResponse(400, "The plan was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateAnnualOhsPlanResource resource, CancellationToken cancellationToken)
    {
        var command = CreateAnnualOhsPlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = AnnualOhsPlanResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(null, created);
    }
}

[ApiController]
[Route("api/v1/predictive_indicators")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Predictive Indicators Endpoints")]
public class PredictiveIndicatorsController(
    IReportsComplianceCommandService commandService,
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

    [HttpPost]
    [SwaggerOperation("Create predictive indicator")]
    [SwaggerResponse(201, "The indicator was created.", typeof(PredictiveIndicatorResource))]
    [SwaggerResponse(400, "The indicator was not created.")]
    public async Task<IActionResult> Create([FromBody] CreatePredictiveIndicatorResource resource, CancellationToken cancellationToken)
    {
        var command = CreatePredictiveIndicatorCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = PredictiveIndicatorResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(null, created);
    }
}

[ApiController]
[Route("api/v1/critical_alerts")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Critical Alerts Endpoints")]
public class CriticalAlertsController(
    IReportsComplianceCommandService commandService,
    IReportsComplianceQueryService queryService) : ControllerBase
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

    [HttpPost]
    [SwaggerOperation("Create critical alert")]
    [SwaggerResponse(201, "The alert was created.", typeof(CriticalAlertResource))]
    [SwaggerResponse(400, "The alert was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateCriticalAlertResource resource, CancellationToken cancellationToken)
    {
        var command = CreateCriticalAlertCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = CriticalAlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(null, created);
    }
}

[ApiController]
[Route("api/v1/generated_reports")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Generated Reports Endpoints")]
public class GeneratedReportsController(
    IReportsComplianceCommandService commandService,
    IReportsComplianceQueryService queryService) : ControllerBase
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
        return CreatedAtAction(null, created);
    }
}

[ApiController]
[Route("api/v1/kpi_dashboard")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("KPI Dashboard Endpoints")]
public class KpiDashboardController(
    IReportsComplianceCommandService commandService,
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

    [HttpPost]
    [SwaggerOperation("Create KPI dashboard entry")]
    [SwaggerResponse(201, "The KPI entry was created.", typeof(KpiDashboardResource))]
    [SwaggerResponse(400, "The KPI entry was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateKpiDashboardResource resource, CancellationToken cancellationToken)
    {
        var command = CreateKpiDashboardCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = KpiDashboardResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(null, created);
    }
}

[ApiController]
[Route("api/v1/historical_trends")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Historical Trends Endpoints")]
public class HistoricalTrendsController(
    IReportsComplianceCommandService commandService,
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

    [HttpPost]
    [SwaggerOperation("Create historical trend")]
    [SwaggerResponse(201, "The trend was created.", typeof(HistoricalTrendResource))]
    [SwaggerResponse(400, "The trend was not created.")]
    public async Task<IActionResult> Create([FromBody] CreateHistoricalTrendResource resource, CancellationToken cancellationToken)
    {
        var command = CreateHistoricalTrendCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await commandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(result.Error);
        var created = HistoricalTrendResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(null, created);
    }
}
