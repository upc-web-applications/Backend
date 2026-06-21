using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;

namespace Acme.Center.Platform.RiskAssessments.Application.QueryServices;

public interface IDailySummaryQueryService
{
    Task<IEnumerable<DailySummary>> Handle(GetAllDailySummariesQuery query, CancellationToken cancellationToken);
    Task<DailySummary?> Handle(GetDailySummaryByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<DailySummary>> Handle(GetDailySummariesBySectorQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<DailySummary>> Handle(GetDailySummariesByDateQuery query, CancellationToken cancellationToken);
}
