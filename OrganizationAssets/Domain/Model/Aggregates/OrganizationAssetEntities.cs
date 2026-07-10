namespace Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;

public class Headquarters
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Area
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int HeadquartersId { get; set; }
    public string Status { get; set; } = "Active";
    public string RiskLevel { get; set; } = "Medium";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Asset
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AreaId { get; set; }
    public int HeadquartersId { get; set; }
    public string Status { get; set; } = "Active";
    public DateTime SystemEntryDate { get; set; } = DateTime.UtcNow;
    public DateTime? DeactivationDate { get; set; }
    public DateTime AcquisitionDate { get; set; } = DateTime.UtcNow;
    public DateTime? LastMaintenanceDate { get; set; }
}
