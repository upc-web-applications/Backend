namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreateCriticalAlertResource(string Type, string Sector, string RiskType, string Message, int ElapsedHours);
