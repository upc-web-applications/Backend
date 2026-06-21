using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;

namespace Acme.Center.Platform.RiskAssessments.Application.QueryServices;

public interface IPatternAlertQueryService
{
    Task<IEnumerable<PatternAlert>> Handle(GetAllPatternAlertsQuery query, CancellationToken cancellationToken);
    Task<PatternAlert?> Handle(GetPatternAlertByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<PatternAlert>> Handle(GetPatternAlertsBySectorQuery query, CancellationToken cancellationToken);
}
