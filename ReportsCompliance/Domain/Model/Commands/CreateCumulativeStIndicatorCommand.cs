namespace RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;

public record CreateCumulativeStIndicatorCommand(string Name, decimal Value, string Status);
