using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;

namespace Acme.Center.Platform.RiskAssessments.Application.QueryServices;

public interface IRiskPatternQueryService
{
    Task<IEnumerable<RiskPattern>> Handle(GetAllRiskPatternsQuery query, CancellationToken cancellationToken);
    Task<RiskPattern?> Handle(GetRiskPatternByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<RiskPattern>> Handle(GetRiskPatternsBySectorQuery query, CancellationToken cancellationToken);
}
