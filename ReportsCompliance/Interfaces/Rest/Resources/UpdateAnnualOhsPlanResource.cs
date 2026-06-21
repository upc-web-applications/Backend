namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record UpdateAnnualOhsPlanResource(
    int Year,
    decimal GlobalCompliance,
    decimal Goal,
    int CompletedActivities,
    int TotalActivities);
