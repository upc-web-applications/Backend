using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class RiskAssessmentResourceFromEntityAssembler
{
    public static RiskAssessmentResource ToResourceFromEntity(RiskAssessment entity)
        => new(entity.Id, entity.Code, entity.Sector, entity.HazardType, entity.Description,
               entity.Probability, entity.Severity, entity.RiskLevel, entity.ControlMeasures,
               entity.Status, entity.EvaluationDate, entity.UserId, entity.CreatedAt, entity.UpdatedAt);
}
