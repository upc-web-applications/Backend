namespace Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;

public record CreateGeneratedReportCommand(string Type, int? Month, int? Year, string Format, string FileName);
