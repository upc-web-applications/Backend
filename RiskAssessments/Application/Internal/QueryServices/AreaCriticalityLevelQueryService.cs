using Acme.Center.Platform.RiskAssessments.Application.QueryServices;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Application.Internal.QueryServices;

public class AreaCriticalityLevelQueryService(IAreaCriticalityLevelRepository repository) : IAreaCriticalityLevelQueryService
{
    public async Task<IEnumerable<AreaCriticalityLevel>> Handle(GetAllAreaCriticalityLevelsQuery query, CancellationToken cancellationToken)
        => await repository.ListAsync(cancellationToken);

    public async Task<AreaCriticalityLevel?> Handle(GetAreaCriticalityLevelByIdQuery query, CancellationToken cancellationToken)
        => await repository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<AreaCriticalityLevel>> Handle(GetAreaCriticalityLevelsBySectorQuery query, CancellationToken cancellationToken)
        => await repository.FindBySectorAsync(query.Sector, cancellationToken);
}
