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
using CorrectiveActionTicket = Acme.Center.Platform.Mitigations.Domain.Model.Aggregates.CorrectiveActionTicket;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest;

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
        var tickets = await OperationalReportsCalculator.LoadCorrectiveTicketsAsync(context, cancellationToken);
        return Ok(OperationalReportsCalculator.BuildHistoricalIncidentRecords(tickets));
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
        var tickets = await OperationalReportsCalculator.LoadCorrectiveTicketsAsync(context, cancellationToken);
        return Ok(OperationalReportsCalculator.BuildAnnualOhsPlan(tickets));
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
    IReportsComplianceQueryService queryService,
    AppDbContext context) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all predictive indicators")]
    [SwaggerResponse(200, "The indicators were found.", typeof(IEnumerable<PredictiveIndicatorResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tickets = await OperationalReportsCalculator.LoadCorrectiveTicketsAsync(context, cancellationToken);
        return Ok(OperationalReportsCalculator.BuildPredictiveIndicators(tickets));
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
        var tickets = await OperationalReportsCalculator.LoadCorrectiveTicketsAsync(context, cancellationToken);
        var candidates = OperationalReportsCalculator.BuildCriticalAlerts(tickets);

        var alerts = await context.Set<CriticalAlert>().ToListAsync(cancellationToken);
        var alertsById = alerts.ToDictionary(alert => alert.Id);

        foreach (var candidate in candidates)
        {
            if (alertsById.TryGetValue(candidate.Id, out var existingAlert))
            {
                existingAlert.ElapsedHours = candidate.ElapsedHours;
                existingAlert.Message = candidate.Message;
            }
            else
            {
                var newAlert = new CriticalAlert
                {
                    Id = candidate.Id,
                    Type = candidate.Type,
                    Sector = candidate.Sector,
                    RiskType = candidate.RiskType,
                    Message = candidate.Message,
                    ElapsedHours = candidate.ElapsedHours,
                    Status = "active",
                    ResponsibleSupervisor = candidate.ResponsibleSupervisor
                };
                await context.Set<CriticalAlert>().AddAsync(newAlert, cancellationToken);
                alerts.Add(newAlert);
            }
        }

        await unitOfWork.CompleteAsync(cancellationToken);

        return Ok(alerts.Select(CriticalAlertResourceFromEntityAssembler.ToResourceFromEntity));
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
    IReportsComplianceQueryService queryService,
    AppDbContext context) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all KPI dashboard entries")]
    [SwaggerResponse(200, "The KPI entries were found.", typeof(IEnumerable<KpiDashboardResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tickets = await OperationalReportsCalculator.LoadCorrectiveTicketsAsync(context, cancellationToken);
        return Ok(OperationalReportsCalculator.BuildKpiDashboard(tickets));
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
    IReportsComplianceQueryService queryService,
    AppDbContext context) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all historical trends")]
    [SwaggerResponse(200, "The trends were found.", typeof(IEnumerable<HistoricalTrendResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tickets = await OperationalReportsCalculator.LoadCorrectiveTicketsAsync(context, cancellationToken);
        return Ok(OperationalReportsCalculator.BuildHistoricalTrends(tickets));
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

internal static class OperationalReportsCalculator
{
    private const decimal OhsGoal = 80m;
    private const decimal ResolvedGoal = 10m;

    public static async Task<List<CorrectiveActionTicket>> LoadCorrectiveTicketsAsync(AppDbContext context, CancellationToken cancellationToken)
    {
        return await context.Set<CorrectiveActionTicket>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public static IEnumerable<KpiDashboardResource> BuildKpiDashboard(IReadOnlyCollection<CorrectiveActionTicket> tickets)
    {
        var total = tickets.Count;
        var resolved = tickets.Count(IsClosed);
        var active = total - resolved;
        var criticalSectors = tickets
            .Where(ticket => !IsClosed(ticket) && (IsCritical(ticket) || ticket.SlaMissed))
            .Select(ticket => NormalizeText(ticket.Sector, "Sin sector"))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Count();
        var compliance = CalculatePercentage(resolved, total);
        var now = DateTime.UtcNow;

        return new[]
        {
            new KpiDashboardResource("active_incidents", "Incidentes activos", active, 0, active == 0 ? "Alerta" : "Critico", now),
            new KpiDashboardResource("resolved_incidents", "Incidentes resueltos", resolved, ResolvedGoal, resolved >= ResolvedGoal ? "Alerta" : "Critico", now),
            new KpiDashboardResource("critical_sectors", "Sectores criticos", criticalSectors, 0, criticalSectors == 0 ? "Alerta" : "Critico", now),
            new KpiDashboardResource("ohs_plan_compliance", "Cumplimiento Plan SST", compliance, OhsGoal, compliance >= OhsGoal ? "Alerta" : "Critico", now)
        };
    }

    public static IEnumerable<AnnualOhsPlanResource> BuildAnnualOhsPlan(IReadOnlyCollection<CorrectiveActionTicket> tickets)
    {
        var year = DateTime.UtcNow.Year;
        var annualTickets = tickets.Where(ticket => ticket.CreatedDate.Year == year).ToList();
        var total = annualTickets.Count;
        var completed = annualTickets.Count(IsClosed);

        return new[]
        {
            new AnnualOhsPlanResource($"ohs-{year}", year, CalculatePercentage(completed, total), OhsGoal, completed, total)
        };
    }

    public static IEnumerable<PredictiveIndicatorResource> BuildPredictiveIndicators(IReadOnlyCollection<CorrectiveActionTicket> tickets)
    {
        var now = DateTime.UtcNow;

        int ElapsedHours(CorrectiveActionTicket ticket) =>
            Math.Max(0, (int)Math.Round(((ticket.ClosureDate ?? now) - ticket.CreatedDate).TotalHours));

        var activeTickets = tickets.Where(ticket => !IsClosed(ticket)).ToList();
        var resolvedTickets = tickets.Where(IsClosed).ToList();

        var sectorsWithTrend = activeTickets
            .GroupBy(ticket => NormalizeText(ticket.Sector, "Sin sector"), StringComparer.OrdinalIgnoreCase)
            .Select(group =>
            {
                var events = group.Count();
                var maxElapsed = group.Max(ElapsedHours);
                var variation = Math.Max(10, Math.Min(99, events * 15 + (int)Math.Round(maxElapsed / 8.0)));
                var status = maxElapsed >= 48 || events >= 3 ? "critical" : "alert";
                return new SectorTrendResource(group.Key, events, variation, status);
            })
            .ToList();

        var recurringTypes = tickets
            .GroupBy(ticket => NormalizeText(ticket.RiskType, "Sin tipo"), StringComparer.OrdinalIgnoreCase)
            .Select(group =>
            {
                var count = group.Count();
                var percentage = tickets.Count > 0 ? (int)Math.Round((decimal)count / tickets.Count * 100) : 0;
                var trend = count >= 2 ? "increasing" : "stable";
                return new RecurringIncidentTypeResource(group.Key, count, percentage, trend);
            })
            .ToList();

        var avgResolution = resolvedTickets.Count > 0
            ? (int)Math.Round(resolvedTickets.Average(ElapsedHours))
            : activeTickets.Count > 0
                ? (int)Math.Round(activeTickets.Average(ElapsedHours))
                : 0;

        var active = activeTickets.Count;
        var slaMissed = tickets.Count(ticket => ticket.SlaMissed);
        var recurringSector = tickets
            .Where(ticket => !string.IsNullOrWhiteSpace(ticket.Sector))
            .GroupBy(ticket => ticket.Sector.Trim(), StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(group => group.Count())
            .Select(group => group.Key)
            .FirstOrDefault() ?? "Sin sector recurrente";

        var trendLabel = slaMissed > 0 || active > 0 ? "Alerta" : "Estable";
        var description = $"{active} tickets correctivos activos, {slaMissed} con SLA incumplido. Sector con mayor recurrencia: {recurringSector}.";

        return new[]
        {
            new PredictiveIndicatorResource(
                "LIVE_CORRECTIVE_FLOW",
                "Riesgo operativo actual",
                description,
                active,
                trendLabel,
                now,
                30,
                tickets.Count,
                Math.Max(0, active * 10),
                avgResolution,
                24,
                sectorsWithTrend,
                recurringTypes,
                Array.Empty<object>())
        };
    }

    public static IEnumerable<CriticalAlertResource> BuildCriticalAlerts(IReadOnlyCollection<CorrectiveActionTicket> tickets)
    {
        return tickets
            .Where(ticket => !IsClosed(ticket) && (IsCritical(ticket) || ticket.SlaMissed))
            .Select(ticket =>
            {
                var elapsedHours = Math.Max(0, (int)Math.Floor((DateTime.UtcNow - ticket.CreatedDate).TotalHours));
                var type = ticket.SlaMissed ? "SLA" : "CRITICAL";
                var message = ticket.SlaMissed
                    ? $"Ticket correctivo #{ticket.TicketNumber} con SLA incumplido requiere decision administrativa."
                    : $"Ticket correctivo #{ticket.TicketNumber} critico requiere decision administrativa.";

                return new CriticalAlertResource(
                    $"OP-COR-{ticket.TicketNumber}",
                    type,
                    NormalizeText(ticket.Sector, "Sin sector"),
                    NormalizeText(ticket.RiskType, "Sin tipo"),
                    message,
                    elapsedHours,
                    "active",
                    NormalizeText(ticket.TechnicianName, "Sin asignar"));
            });
    }

    public static IEnumerable<HistoricalIncidentRecordResource> BuildHistoricalIncidentRecords(IReadOnlyCollection<CorrectiveActionTicket> tickets)
    {
        return tickets.Select(ticket =>
        {
            var closed = IsClosed(ticket);
            int? resolutionHours = closed && ticket.ClosureDate.HasValue
                ? Math.Max(0, (int)Math.Round((ticket.ClosureDate.Value - ticket.CreatedDate).TotalHours))
                : null;

            return new HistoricalIncidentRecordResource(
                $"INC-COR-{ticket.TicketNumber}",
                NormalizeText(ticket.Sector, "Sin sector"),
                NormalizeText(ticket.RiskType, "Sin tipo"),
                NormalizeCriticality(ticket.CriticalityLevel),
                ticket.CreatedDate,
                string.IsNullOrWhiteSpace(ticket.Instructions) ? $"Ticket correctivo #{ticket.TicketNumber} requiere seguimiento." : ticket.Instructions,
                closed,
                closed ? ticket.ClosureDate : null,
                resolutionHours,
                string.IsNullOrWhiteSpace(ticket.AssignedTechnicianId) ? null : $"OP_{ticket.AssignedTechnicianId}");
        });
    }

    private static string NormalizeCriticality(string value)
    {
        var normalized = value.Trim().ToLowerInvariant();
        if (normalized.Contains("critico") || normalized.Contains("crítico") || normalized.Contains("critical")) return "CRITICAL";
        if (normalized.Contains("importante") || normalized.Contains("alto")) return "HIGH";
        if (normalized.Contains("moderado") || normalized.Contains("medio")) return "MEDIUM";
        return "LOW";
    }

    public static IEnumerable<HistoricalTrendResource> BuildHistoricalTrends(IReadOnlyCollection<CorrectiveActionTicket> tickets)
    {
        return tickets
            .GroupBy(ticket => new
            {
                ticket.CreatedDate.Month,
                ticket.CreatedDate.Year,
                Sector = NormalizeText(ticket.Sector, "Sin sector"),
                Type = NormalizeText(ticket.RiskType, "Sin tipo")
            })
            .OrderBy(group => group.Key.Year)
            .ThenBy(group => group.Key.Month)
            .ThenBy(group => group.Key.Sector)
            .Select(group => new HistoricalTrendResource(
                $"{group.Key.Year}-{group.Key.Month:00}-{Slug(group.Key.Sector)}-{Slug(group.Key.Type)}",
                group.Key.Month,
                group.Key.Year,
                group.Count(),
                group.Key.Sector,
                group.Key.Type));
    }

    private static bool IsClosed(CorrectiveActionTicket ticket)
    {
        return ticket.Status.Contains("cerrado", StringComparison.OrdinalIgnoreCase)
               || ticket.Status.Contains("closed", StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsCritical(CorrectiveActionTicket ticket)
    {
        return ticket.CriticalityLevel.Contains("critico", StringComparison.OrdinalIgnoreCase)
               || ticket.CriticalityLevel.Contains("crítico", StringComparison.OrdinalIgnoreCase)
               || ticket.CriticalityLevel.Contains("critical", StringComparison.OrdinalIgnoreCase);
    }

    private static decimal CalculatePercentage(int partial, int total)
    {
        return total == 0 ? 0 : Math.Round((decimal)partial / total * 100, 0);
    }

    private static string NormalizeText(string value, string fallback)
    {
        return string.IsNullOrWhiteSpace(value) ? fallback : value.Trim();
    }

    private static string Slug(string value)
    {
        return new string(value.ToLowerInvariant().Select(character => char.IsLetterOrDigit(character) ? character : '-').ToArray())
            .Trim('-');
    }
}
