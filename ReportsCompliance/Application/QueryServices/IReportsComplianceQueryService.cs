using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Queries;

namespace RiskGuard.Platform.ReportsCompliance.Application.QueryServices;

public interface IReportsComplianceQueryService
{
    Task<IEnumerable<MonthlyReport>> Handle(GetAllMonthlyReportsQuery query, CancellationToken cancellationToken);
    Task<MonthlyReport?> Handle(GetMonthlyReportByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<MonthlyReport>> Handle(GetMonthlyReportsByYearQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<CumulativeStIndicator>> Handle(GetAllCumulativeStIndicatorsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<HistoricalIncidentRecord>> Handle(GetAllHistoricalIncidentRecordsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<AnnualOhsPlan>> Handle(GetAllAnnualOhsPlansQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<PredictiveIndicator>> Handle(GetAllPredictiveIndicatorsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<CriticalAlert>> Handle(GetAllCriticalAlertsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<GeneratedReport>> Handle(GetAllGeneratedReportsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<KpiDashboard>> Handle(GetAllKpiDashboardQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<HistoricalTrend>> Handle(GetAllHistoricalTrendsQuery query, CancellationToken cancellationToken);
}
