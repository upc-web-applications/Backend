using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class GeneratedReportResourceFromEntityAssembler
{
    public static GeneratedReportResource ToResourceFromEntity(GeneratedReport entity)
    {
        return new GeneratedReportResource(
            entity.Id, entity.Type, entity.Month, entity.Year, entity.Format, entity.GenerationDate, entity.FileName, entity.Status,
            entity.StartDate, entity.EndDate, entity.SectorFilter, entity.SizeKb);
    }
}
