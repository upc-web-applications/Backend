namespace Acme.Center.Platform.RiskAssessments.Domain.Services;

public static class IpercEngine
{
    private static readonly Dictionary<int, Dictionary<int, string>> Matrix = new()
    {
        { 1, new() { { 1, "Low" }, { 2, "Low" }, { 3, "Medium" }, { 4, "High" }, { 5, "High" } } },
        { 2, new() { { 1, "Low" }, { 2, "Medium" }, { 3, "Medium" }, { 4, "High" }, { 5, "Critical" } } },
        { 3, new() { { 1, "Medium" }, { 2, "Medium" }, { 3, "High" }, { 4, "High" }, { 5, "Critical" } } },
        { 4, new() { { 1, "Medium" }, { 2, "High" }, { 3, "High" }, { 4, "Critical" }, { 5, "Critical" } } },
        { 5, new() { { 1, "High" }, { 2, "High" }, { 3, "Critical" }, { 4, "Critical" }, { 5, "Critical" } } }
    };

    public static string CalculateRiskLevel(int probability, int severity)
    {
        if (probability < 1 || probability > 5 || severity < 1 || severity > 5)
            throw new ArgumentOutOfRangeException("Probability and severity must be between 1 and 5.");
        return Matrix[probability][severity];
    }

    public static string GetRiskColor(string riskLevel) => riskLevel switch
    {
        "Low" => "#4CAF50",
        "Medium" => "#FF9800",
        "High" => "#F44336",
        "Critical" => "#9C27B0",
        _ => "#9E9E9E"
    };
}
