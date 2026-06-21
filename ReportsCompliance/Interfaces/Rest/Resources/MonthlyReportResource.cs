namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record MonthlyReportResource(string Id, int Month, int Year, string Status, DateTime CreationDate);
