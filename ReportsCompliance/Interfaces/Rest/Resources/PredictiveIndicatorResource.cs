using System.Text.Json.Serialization;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record SectorTrendResource(
    [property: JsonPropertyName("sector")] string Sector,
    [property: JsonPropertyName("events")] int Events,
    [property: JsonPropertyName("variation_percentage")] int VariationPercentage,
    [property: JsonPropertyName("status")] string Status);

public record RecurringIncidentTypeResource(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("count")] int Count,
    [property: JsonPropertyName("percentage")] int Percentage,
    [property: JsonPropertyName("trend")] string Trend);

public record PredictiveIndicatorResource(
    string Id,
    string Name,
    string Description,
    decimal Value,
    string Trend,
    [property: JsonPropertyName("calculation_date")] DateTime CalculationDate,
    [property: JsonPropertyName("period_days")] int PeriodDays,
    [property: JsonPropertyName("total_incidents")] int TotalIncidents,
    [property: JsonPropertyName("previous_month_variation")] int PreviousMonthVariation,
    [property: JsonPropertyName("average_resolution_time_hours")] int AverageResolutionTimeHours,
    [property: JsonPropertyName("resolution_goal_hours")] int ResolutionGoalHours,
    [property: JsonPropertyName("sectors_with_increasing_trend")] IEnumerable<SectorTrendResource> SectorsWithIncreasingTrend,
    [property: JsonPropertyName("recurring_incident_types")] IEnumerable<RecurringIncidentTypeResource> RecurringIncidentTypes,
    [property: JsonPropertyName("resolution_time_by_type")] IEnumerable<object> ResolutionTimeByType);
