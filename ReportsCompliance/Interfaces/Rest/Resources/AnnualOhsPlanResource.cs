namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record AnnualOhsPlanResource(string Id, int Year, decimal GlobalCompliance, decimal Goal, int CompletedActivities, int TotalActivities);
