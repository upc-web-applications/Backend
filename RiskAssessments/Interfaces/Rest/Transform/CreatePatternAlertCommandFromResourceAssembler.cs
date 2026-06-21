using Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class CreatePatternAlertCommandFromResourceAssembler
{
    public static CreatePatternAlertCommand ToCommandFromResource(CreatePatternAlertResource resource)
        => new(resource.PatternId, resource.SectorId, resource.Sector, resource.RiskType,
               resource.OccurrenceCount, resource.FirstReportDate, resource.Status, resource.GenerationDate);
}
