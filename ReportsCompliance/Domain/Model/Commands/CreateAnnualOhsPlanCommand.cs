namespace Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;

public record CreateAnnualOhsPlanCommand(int Year, decimal GlobalCompliance, decimal Goal, int CompletedActivities, int TotalActivities);
