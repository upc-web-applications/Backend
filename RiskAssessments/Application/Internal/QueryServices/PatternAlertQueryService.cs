using Acme.Center.Platform.RiskAssessments.Application.QueryServices;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Application.Internal.QueryServices;

public class PatternAlertQueryService(IPatternAlertRepository repository) : IPatternAlertQueryService
{
    public async Task<IEnumerable<PatternAlert>> Handle(GetAllPatternAlertsQuery query, CancellationToken cancellationToken)
        => await repository.ListAsync(cancellationToken);

    public async Task<PatternAlert?> Handle(GetPatternAlertByIdQuery query, CancellationToken cancellationToken)
        => await repository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<PatternAlert>> Handle(GetPatternAlertsBySectorQuery query, CancellationToken cancellationToken)
        => await repository.FindBySectorAsync(query.Sector, cancellationToken);
}
