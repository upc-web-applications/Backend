namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CriticalAlertResource(string Id, string Type, string Sector, string RiskType, string Message, int ElapsedHours, string Status, string ResponsibleSupervisor);
