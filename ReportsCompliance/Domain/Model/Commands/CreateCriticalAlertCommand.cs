namespace Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;

public record CreateCriticalAlertCommand(string Type, string Sector, string RiskType, string Message, int ElapsedHours);
