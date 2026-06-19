using System.Text.Json.Serialization;

namespace RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;

public class MonthlyReport
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public int Month { get; set; }
    public int Year { get; set; }
    public string Status { get; set; } = "generated";
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
}

public class CumulativeStIndicator
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Status { get; set; } = "ok";
}

public class HistoricalIncidentRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Sector { get; set; } = string.Empty;
    public string IncidentType { get; set; } = string.Empty;
    public string Criticality { get; set; } = string.Empty;
    public DateTime IncidentDate { get; set; } = DateTime.UtcNow;
}

public class AnnualOhsPlan
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public int Year { get; set; }

    [JsonPropertyName("global_compliance")]
    public decimal GlobalCompliance { get; set; }

    public decimal Goal { get; set; }

    [JsonPropertyName("completed_activities")]
    public int CompletedActivities { get; set; }

    [JsonPropertyName("total_activities")]
    public int TotalActivities { get; set; }
}

public class PredictiveIndicator
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Trend { get; set; } = "stable";
}

public class CriticalAlert
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Type { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;

    [JsonPropertyName("risk_type")]
    public string RiskType { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("elapsed_hours")]
    public int ElapsedHours { get; set; }

    public string Status { get; set; } = "active";
}
