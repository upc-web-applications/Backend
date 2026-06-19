using RiskGuard.Platform.ReportsCompliance.Application.QueryServices;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Queries;
using RiskGuard.Platform.ReportsCompliance.Domain.Repositories;

namespace RiskGuard.Platform.ReportsCompliance.Application.Internal.QueryServices;

public class ReportsComplianceQueryService(IMonthlyReportRepository monthlyReportRepository,
    IBaseReportsRepository baseReportsRepository)
    : IReportsComplianceQueryService
{
    public async Task<IEnumerable<MonthlyReport>> Handle(GetAllMonthlyReportsQuery query, CancellationToken cancellationToken)
    {
        return await monthlyReportRepository.ListAsync(cancellationToken);
    }

    public async Task<MonthlyReport?> Handle(GetMonthlyReportByIdQuery query, CancellationToken cancellationToken)
    {
        return await monthlyReportRepository.FindByIdAsync(query.ReportId, cancellationToken);
    }

    public async Task<IEnumerable<MonthlyReport>> Handle(GetMonthlyReportsByYearQuery query, CancellationToken cancellationToken)
    {
        return await monthlyReportRepository.ListByYearAsync(query.Year, cancellationToken);
    }

    public async Task<IEnumerable<CumulativeStIndicator>> Handle(GetAllCumulativeStIndicatorsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllCumulativeStIndicatorsAsync(cancellationToken);
    }

    public async Task<IEnumerable<HistoricalIncidentRecord>> Handle(GetAllHistoricalIncidentRecordsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllHistoricalIncidentRecordsAsync(cancellationToken);
    }

    public async Task<IEnumerable<AnnualOhsPlan>> Handle(GetAllAnnualOhsPlansQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllAnnualOhsPlansAsync(cancellationToken);
    }

    public async Task<IEnumerable<PredictiveIndicator>> Handle(GetAllPredictiveIndicatorsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllPredictiveIndicatorsAsync(cancellationToken);
    }

    public async Task<IEnumerable<CriticalAlert>> Handle(GetAllCriticalAlertsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllCriticalAlertsAsync(cancellationToken);
    }

    public async Task<IEnumerable<GeneratedReport>> Handle(GetAllGeneratedReportsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllGeneratedReportsAsync(cancellationToken);
    }

    public async Task<IEnumerable<KpiDashboard>> Handle(GetAllKpiDashboardQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllKpiDashboardAsync(cancellationToken);
    }

    public async Task<IEnumerable<HistoricalTrend>> Handle(GetAllHistoricalTrendsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllHistoricalTrendsAsync(cancellationToken);
    }
}
