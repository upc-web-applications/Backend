using Acme.Center.Platform.RiskAssessments.Domain.Model.Commands;
using Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.RiskAssessments.Interfaces.Rest.Transform;

public static class CreateAreaCriticalityLevelCommandFromResourceAssembler
{
    public static CreateAreaCriticalityLevelCommand ToCommandFromResource(CreateAreaCriticalityLevelResource resource)
        => new(resource.SectorId, resource.Sector, resource.CriticalityLevel, resource.MapIntensity, resource.LastUpdated);
}
