namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record HistoricalTrendResource(string Id, int Month, int Year, int TotalIncidents, string Sector, string Type);
