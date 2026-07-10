using System.Text.Json.Serialization;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record HistoricalTrendResource(
    string Id,
    int Month,
    int Year,
    [property: JsonPropertyName("total_incidents")] int TotalIncidents,
    [property: JsonPropertyName("incidents_by_type")] IDictionary<string, int> IncidentsByType,
    [property: JsonPropertyName("incidents_by_sector")] IDictionary<string, int> IncidentsBySector);
