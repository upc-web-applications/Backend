using Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class CreateDailySummaryCommandFromResourceAssembler
{
    public static CreateDailySummaryCommand ToCommandFromResource(CreateDailySummaryResource resource)
        => new(resource.Date, resource.SectorId, resource.Sector, resource.TotalNew,
               resource.TotalInProgress, resource.TotalResolved);
}
