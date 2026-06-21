using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class UpdateCriticalAlertCommandFromResourceAssembler
{
    public static CriticalAlert ToEntityFromResource(string id, UpdateCriticalAlertResource resource)
    {
        return new CriticalAlert
        {
            Id = id,
            Type = resource.Type,
            Sector = resource.Sector,
            RiskType = resource.RiskType,
            Message = resource.Message,
            ElapsedHours = resource.ElapsedHours,
            Status = resource.Status
        };
    }
}
