using System.Text.Json.Serialization;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CriticalAlertResource(
    string Id, string Type, string Sector,
    [property: JsonPropertyName("risk_type")] string RiskType,
    string Message,
    [property: JsonPropertyName("elapsed_hours")] int ElapsedHours,
    string Status,
    [property: JsonPropertyName("responsible_supervisor")] string ResponsibleSupervisor,
    [property: JsonPropertyName("creation_date")] DateTime CreationDate);
