namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record UpdateMonthlyReportResource(
    int Month,
    int Year,
    string Status);
