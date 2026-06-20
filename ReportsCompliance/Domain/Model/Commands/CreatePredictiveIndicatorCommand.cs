namespace RiskGuard.Platform.ReportsCompliance.Domain.Model.Commands;

public record CreatePredictiveIndicatorCommand(string Name, string Description, decimal Value, string Trend);
