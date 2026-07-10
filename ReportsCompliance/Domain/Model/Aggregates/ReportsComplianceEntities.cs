using System.Text.Json.Serialization;
using Acme.Center.Platform.Shared.Domain.Model.Entities;

namespace Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;

public class HistoricalIncidentRecord : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Sector { get; set; } = string.Empty;
    public string IncidentType { get; set; } = string.Empty;
    public string Criticality { get; set; } = string.Empty;
    public DateTime IncidentDate { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = string.Empty;
    public bool Resolved { get; set; } = false;
    public DateTime? ClosingDate { get; set; }
    public int? ResolutionTimeHours { get; set; }
    public string? OperatorId { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class AnnualOhsPlan : IAuditableEntity
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

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class PredictiveIndicator : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Trend { get; set; } = "stable";
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class CriticalAlert : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Type { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;

    [JsonPropertyName("risk_type")]
    public string RiskType { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("elapsed_hours")]
    public int ElapsedHours { get; set; }

    public string Status { get; set; } = "unresolved";

    [JsonPropertyName("responsible_supervisor")]
    public string ResponsibleSupervisor { get; set; } = string.Empty;

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class GeneratedReport : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Type { get; set; } = string.Empty;
    public int? Month { get; set; }
    public int? Year { get; set; }
    public string Format { get; set; } = "pdf";

    [JsonPropertyName("generation_date")]
    public DateTime GenerationDate { get; set; } = DateTime.UtcNow;

    [JsonPropertyName("file_name")]
    public string FileName { get; set; } = string.Empty;

    public string Status { get; set; } = "generated";

    [JsonPropertyName("start_date")]
    public DateTime? StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName("sector_filter")]
    public string? SectorFilter { get; set; }

    [JsonPropertyName("size_kb")]
    public int? SizeKb { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class KpiDashboard : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Goal { get; set; }
    public string Status { get; set; } = "ok";

    [JsonPropertyName("update_date")]
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class MonthlyReport : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public int Month { get; set; }
    public int Year { get; set; }

    [JsonPropertyName("total_incidents")]
    public int TotalIncidents { get; set; }

    [JsonPropertyName("resolved_incidents")]
    public int ResolvedIncidents { get; set; }

    [JsonPropertyName("compliance_percentage")]
    public decimal CompliancePercentage { get; set; }

    public string Status { get; set; } = "draft";

    [JsonPropertyName("generated_at")]
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class CumulativeStIndicator : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("total_incidents")]
    public int TotalIncidents { get; set; }

    [JsonPropertyName("resolved_incidents")]
    public int ResolvedIncidents { get; set; }

    [JsonPropertyName("compliance_rate")]
    public decimal ComplianceRate { get; set; }

    public string Period { get; set; } = string.Empty;

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}

public class HistoricalTrend : IAuditableEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public int Month { get; set; }
    public int Year { get; set; }

    [JsonPropertyName("total_incidents")]
    public int TotalIncidents { get; set; }

    public string Sector { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
