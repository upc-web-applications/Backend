namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record UpdateHistoricalIncidentRecordResource(
    string Sector,
    string IncidentType,
    string Criticality,
    DateTime IncidentDate);
