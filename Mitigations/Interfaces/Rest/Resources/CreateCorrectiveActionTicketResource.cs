namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

public record CreateCorrectiveActionTicketResource(
    int TicketNumber, string? ReportId, string? SectorId, string Sector,
    string RiskType, string CriticalityLevel, string Status,
    string Instructions, string? AssignedTechnicianId, string TechnicianName,
    DateTime CreatedDate, DateTime? ClosureDate,
    int SlaLimitHours, bool SlaMissed);
