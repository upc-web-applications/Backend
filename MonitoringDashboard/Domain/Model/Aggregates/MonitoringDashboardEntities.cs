namespace RiskGuard.Platform.MonitoringDashboard.Domain.Model.Aggregates;

public class HeatMapZone
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SectorId { get; set; }
    public decimal HeatIndex { get; set; }
    public string RiskLevel { get; set; } = "Medio";
    public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
}

public class Ticket
{
    public int Id { get; set; }
    public int SectorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = "Pendiente";
    public string Priority { get; set; } = "Media";
    public string AssignedTechnicianId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Technician
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string Status { get; set; } = "Activo";
}

public class Asset
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int SectorId { get; set; }
    public string Status { get; set; } = "Activo";
}

public class PreventiveMaintenance
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string AssetId { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "Programado";
    public DateTime ScheduledDate { get; set; } = DateTime.UtcNow;
}

public class ArchivedReport
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string HashIntegrity { get; set; } = string.Empty;
    public DateTime ArchiveDate { get; set; } = DateTime.UtcNow;
}
