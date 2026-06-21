namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreateHistoricalTrendResource(int Month, int Year, int TotalIncidents, string Sector, string Type);
