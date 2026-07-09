using System.Text.Json.Serialization;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record KpiDashboardResource(
    string Id, string Name, decimal Value, decimal Goal, string Status,
    [property: JsonPropertyName("update_date")] DateTime UpdateDate);
