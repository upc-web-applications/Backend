namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record HistoricalIncidentRecordResource(
    string Id,
    string Sector,
    string IncidentType,
    string Criticality,
    DateTime IncidentDate,
    string Description,
    bool Resolved,
    DateTime? ClosingDate,
    int? ResolutionTimeHours,
    string? OperatorId);
