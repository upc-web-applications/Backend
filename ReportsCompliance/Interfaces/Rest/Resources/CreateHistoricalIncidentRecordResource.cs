namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreateHistoricalIncidentRecordResource(string Sector, string IncidentType, string Criticality);
