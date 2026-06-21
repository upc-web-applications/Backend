using Acme.Center.Platform.RiskAssessments.Application.QueryServices;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Application.Internal.QueryServices;

public class RiskPatternQueryService(IRiskPatternRepository repository) : IRiskPatternQueryService
{
    public async Task<IEnumerable<RiskPattern>> Handle(GetAllRiskPatternsQuery query, CancellationToken cancellationToken)
        => await repository.ListAsync(cancellationToken);

    public async Task<RiskPattern?> Handle(GetRiskPatternByIdQuery query, CancellationToken cancellationToken)
        => await repository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<RiskPattern>> Handle(GetRiskPatternsBySectorQuery query, CancellationToken cancellationToken)
        => await repository.FindBySectorAsync(query.Sector, cancellationToken);
}
