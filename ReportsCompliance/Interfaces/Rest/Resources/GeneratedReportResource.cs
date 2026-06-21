namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record GeneratedReportResource(string Id, string Type, int? Month, int? Year, string Format, DateTime GenerationDate, string FileName, string Status);
