namespace RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;

public class MonthlyReport
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public int Month { get; set; }
    public int Year { get; set; }
    public string Status { get; set; } = "generated";
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}
