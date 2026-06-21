namespace Acme.Center.Platform.RiskAssessments.Domain.Model.ValueObjects;

public record RiskLevel(string Level)
{
    public RiskLevel() : this(string.Empty) { }

    public static RiskLevel Low => new("Low");
    public static RiskLevel Medium => new("Medium");
    public static RiskLevel High => new("High");
    public static RiskLevel Critical => new("Critical");
}
