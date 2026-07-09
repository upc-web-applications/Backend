using System.Text.Json.Serialization;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record MonthlyOhsDetailResource(
    int Month,
    [property: JsonPropertyName("completed_activities")] int CompletedActivities,
    [property: JsonPropertyName("planned_activities")] int PlannedActivities,
    int Compliance,
    string Status);

public record SectorOhsDetailResource(
    string Sector,
    [property: JsonPropertyName("completed_activities")] int CompletedActivities,
    [property: JsonPropertyName("planned_activities")] int PlannedActivities,
    int Compliance);

public record AnnualOhsPlanResource(
    string Id,
    int Year,
    [property: JsonPropertyName("global_compliance")] decimal GlobalCompliance,
    decimal Goal,
    [property: JsonPropertyName("completed_activities")] int CompletedActivities,
    [property: JsonPropertyName("total_activities")] int TotalActivities,
    [property: JsonPropertyName("critical_months")] int CriticalMonths,
    [property: JsonPropertyName("update_date")] DateTime UpdateDate,
    [property: JsonPropertyName("monthly_details")] IEnumerable<MonthlyOhsDetailResource> MonthlyDetails,
    [property: JsonPropertyName("details_by_sector")] IEnumerable<SectorOhsDetailResource> DetailsBySector,
    [property: JsonPropertyName("next_review")] DateTime? NextReview);
