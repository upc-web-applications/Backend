namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

public record PredictiveIndicatorResource(string Id, string Name, string Description, decimal Value, string Trend);
