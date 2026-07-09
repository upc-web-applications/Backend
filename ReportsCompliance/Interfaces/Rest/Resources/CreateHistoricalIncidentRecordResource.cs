namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreateHistoricalIncidentRecordResource(
    string Sector,
    string IncidentType,
    string Criticality,
    string Description,
    bool Resolved,
    DateTime? ClosingDate,
    int? ResolutionTimeHours,
    string? OperatorId);
