using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class CorrectiveActionTicketResourceFromEntityAssembler
{
    public static CorrectiveActionTicketResource ToResourceFromEntity(CorrectiveActionTicket e)
        => new(e.Id, e.TicketNumber, e.ReportId, e.SectorId, e.Sector, e.RiskType, e.CriticalityLevel,
               e.Status, e.Instructions, e.AssignedTechnicianId, e.TechnicianName,
               e.CreatedDate, e.ClosureDate, e.SlaLimitHours, e.SlaMissed,
               e.CreatedAt, e.UpdatedAt);
}
