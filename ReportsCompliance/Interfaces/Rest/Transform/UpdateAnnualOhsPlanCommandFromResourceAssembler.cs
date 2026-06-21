using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.ReportsCompliance.Interfaces.Rest.Transform;

public static class UpdateAnnualOhsPlanCommandFromResourceAssembler
{
    public static AnnualOhsPlan ToEntityFromResource(string id, UpdateAnnualOhsPlanResource resource)
    {
        return new AnnualOhsPlan
        {
            Id = id,
            Year = resource.Year,
            GlobalCompliance = resource.GlobalCompliance,
            Goal = resource.Goal,
            CompletedActivities = resource.CompletedActivities,
            TotalActivities = resource.TotalActivities
        };
    }
}
