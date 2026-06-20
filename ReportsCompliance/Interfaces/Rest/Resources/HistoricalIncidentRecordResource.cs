namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record HistoricalIncidentRecordResource(string Id, string Sector, string IncidentType, string Criticality, DateTime IncidentDate);
