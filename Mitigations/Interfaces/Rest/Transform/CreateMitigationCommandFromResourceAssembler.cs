using Acme.Center.Platform.Mitigations.Domain.Model.Commands;
using Acme.Center.Platform.Mitigations.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Mitigations.Interfaces.Rest.Transform;

public static class CreateMitigationCommandFromResourceAssembler
{
    public static CreateMitigationCommand ToCommandFromResource(CreateMitigationResource r)
        => new(r.RiskAssessmentId, r.TicketId, r.Code, r.Description, r.Responsible, r.AssignedDate, r.ExecutionDate, r.Status, r.Result, r.Observations);
}
