namespace Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;

public record CreateKpiDashboardCommand(string Name, decimal Value, decimal Goal, string Status);
