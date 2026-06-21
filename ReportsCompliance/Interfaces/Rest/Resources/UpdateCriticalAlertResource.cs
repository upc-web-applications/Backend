namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record UpdateCriticalAlertResource(
    string Type,
    string Sector,
    string RiskType,
    string Message,
    int ElapsedHours,
    string Status);
