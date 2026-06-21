using Acme.Center.Platform.RiskAssessments.Application.QueryServices;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Queries;
using Acme.Center.Platform.RiskAssessments.Domain.Repositories;

namespace Acme.Center.Platform.RiskAssessments.Application.Internal.QueryServices;

public class DailySummaryQueryService(IDailySummaryRepository repository) : IDailySummaryQueryService
{
    public async Task<IEnumerable<DailySummary>> Handle(GetAllDailySummariesQuery query, CancellationToken cancellationToken)
        => await repository.ListAsync(cancellationToken);

    public async Task<DailySummary?> Handle(GetDailySummaryByIdQuery query, CancellationToken cancellationToken)
        => await repository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<DailySummary>> Handle(GetDailySummariesBySectorQuery query, CancellationToken cancellationToken)
        => await repository.FindBySectorAsync(query.Sector, cancellationToken);

    public async Task<IEnumerable<DailySummary>> Handle(GetDailySummariesByDateQuery query, CancellationToken cancellationToken)
        => await repository.FindByDateAsync(query.Date, cancellationToken);
}
