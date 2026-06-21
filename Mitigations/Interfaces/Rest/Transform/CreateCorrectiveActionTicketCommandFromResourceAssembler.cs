using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class CreateCorrectiveActionTicketCommandFromResourceAssembler
{
    public static CreateCorrectiveActionTicketCommand ToCommandFromResource(CreateCorrectiveActionTicketResource r)
        => new(r.TicketNumber, r.ReportId, r.SectorId, r.Sector, r.RiskType, r.CriticalityLevel, r.Status,
               r.Instructions, r.AssignedTechnicianId, r.TechnicianName, r.CreatedDate, r.ClosureDate,
               r.SlaLimitHours, r.SlaMissed);
}
