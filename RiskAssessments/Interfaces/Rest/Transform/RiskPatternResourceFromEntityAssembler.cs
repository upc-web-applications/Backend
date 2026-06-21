using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class RiskPatternResourceFromEntityAssembler
{
    public static RiskPatternResource ToResourceFromEntity(RiskPattern entity)
        => new(entity.Id, entity.SectorId, entity.Sector, entity.IncidentType, entity.HazardType,
               entity.Description, entity.Frequency, entity.FirstOccurrenceDate,
               entity.AnalysisPeriodDays, entity.IsReviewed, entity.ReviewDate, entity.ReviewedBy,
               entity.CreatedAt, entity.UpdatedAt);
}
