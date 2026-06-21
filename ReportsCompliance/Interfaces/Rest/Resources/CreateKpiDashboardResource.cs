namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreateKpiDashboardResource(string Name, decimal Value, decimal Goal, string Status);
