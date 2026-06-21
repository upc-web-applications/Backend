using Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class CreateRiskPatternCommandFromResourceAssembler
{
    public static CreateRiskPatternCommand ToCommandFromResource(CreateRiskPatternResource resource)
        => new(resource.SectorId, resource.Sector, resource.IncidentType, resource.HazardType,
               resource.Description, resource.Frequency, resource.FirstOccurrenceDate,
               resource.AnalysisPeriodDays, resource.IsReviewed, resource.ReviewDate, resource.ReviewedBy);
}
