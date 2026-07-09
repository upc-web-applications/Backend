using System.Text.Json.Serialization;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record HistoricalIncidentRecordResource(
    string Id,
    string Sector,
    [property: JsonPropertyName("incident_type")] string IncidentType,
    string Criticality,
    [property: JsonPropertyName("date")] DateTime IncidentDate,
    string Description,
    bool Resolved,
    [property: JsonPropertyName("closing_date")] DateTime? ClosingDate,
    [property: JsonPropertyName("resolution_time_hours")] int? ResolutionTimeHours,
    [property: JsonPropertyName("operator_id")] string? OperatorId);
