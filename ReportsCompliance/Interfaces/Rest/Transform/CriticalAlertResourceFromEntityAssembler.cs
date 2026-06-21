using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class CriticalAlertResourceFromEntityAssembler
{
    public static CriticalAlertResource ToResourceFromEntity(CriticalAlert entity)
    {
        return new CriticalAlertResource(entity.Id, entity.Type, entity.Sector, entity.RiskType, entity.Message, entity.ElapsedHours, entity.Status);
    }
}
