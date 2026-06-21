namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record CreatePredictiveIndicatorResource(string Name, string Description, decimal Value, string Trend);
