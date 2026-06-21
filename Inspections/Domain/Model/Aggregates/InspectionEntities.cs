namespace Acme.Center.Platform.Inspections.Domain.Model.Aggregates;

public class Inspection
{
    public int Id { get; set; }
    public string Ticket { get; set; } = string.Empty;
    public string IncidentType { get; set; } = string.Empty;
    public int AreaId { get; set; }
    public int HeadquartersId { get; set; }
    public int? AssetId { get; set; }
    public string UrgencyLevel { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public string? PhotoUrl { get; set; }
    public string OperatorId { get; set; } = string.Empty;
    public DateTime ReportDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    public string? CorrectiveAction { get; set; }
}

public class Danger
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class PhotoEvidence
{
    public int Id { get; set; }
    public int InspectionId { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
}
