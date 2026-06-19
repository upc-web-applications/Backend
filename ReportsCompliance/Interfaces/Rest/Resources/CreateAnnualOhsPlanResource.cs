namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreateAnnualOhsPlanResource(int Year, decimal GlobalCompliance, decimal Goal, int CompletedActivities, int TotalActivities);
