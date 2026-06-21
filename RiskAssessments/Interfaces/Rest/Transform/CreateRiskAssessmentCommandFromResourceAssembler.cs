using Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class CreateRiskAssessmentCommandFromResourceAssembler
{
    public static CreateRiskAssessmentCommand ToCommandFromResource(CreateRiskAssessmentResource resource)
        => new(resource.Code, resource.Sector, resource.HazardType, resource.Description,
               resource.Probability, resource.Severity, resource.RiskLevel, resource.ControlMeasures,
               resource.Status, resource.EvaluationDate, resource.UserId);
}
