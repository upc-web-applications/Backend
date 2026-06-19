namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreateGeneratedReportResource(string Type, int? Month, int? Year, string Format, string FileName);
