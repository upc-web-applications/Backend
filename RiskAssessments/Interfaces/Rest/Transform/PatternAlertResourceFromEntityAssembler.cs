using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class PatternAlertResourceFromEntityAssembler
{
    public static PatternAlertResource ToResourceFromEntity(PatternAlert entity)
        => new(entity.Id, entity.PatternId, entity.SectorId, entity.Sector, entity.RiskType,
               entity.OccurrenceCount, entity.FirstReportDate, entity.Status, entity.GenerationDate,
               entity.CreatedAt, entity.UpdatedAt);
}
