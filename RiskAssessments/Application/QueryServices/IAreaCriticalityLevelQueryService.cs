using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;

namespace Acme.Center.Platform.RiskAssessments.Application.QueryServices;

public interface IAreaCriticalityLevelQueryService
{
    Task<IEnumerable<AreaCriticalityLevel>> Handle(GetAllAreaCriticalityLevelsQuery query, CancellationToken cancellationToken);
    Task<AreaCriticalityLevel?> Handle(GetAreaCriticalityLevelByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<AreaCriticalityLevel>> Handle(GetAreaCriticalityLevelsBySectorQuery query, CancellationToken cancellationToken);
}
