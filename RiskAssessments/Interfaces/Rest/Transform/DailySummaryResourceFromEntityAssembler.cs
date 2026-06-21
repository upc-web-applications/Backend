using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class DailySummaryResourceFromEntityAssembler
{
    public static DailySummaryResource ToResourceFromEntity(DailySummary entity)
        => new(entity.Id, entity.Date, entity.SectorId, entity.Sector, entity.TotalNew,
               entity.TotalInProgress, entity.TotalResolved, entity.CreatedAt, entity.UpdatedAt);
}
