namespace RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;

public record CreateHistoricalTrendCommand(int Month, int Year, int TotalIncidents, string Sector, string Type);
