namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record KpiDashboardResource(string Id, string Name, decimal Value, decimal Goal, string Status, DateTime UpdateDate);
