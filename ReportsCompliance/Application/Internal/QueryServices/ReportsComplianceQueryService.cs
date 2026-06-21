using Acme.Center.Platform.ReportsCompliance.Application.QueryServices;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Queries;
using Acme.Center.Platform.ReportsCompliance.Domain.Repositories;

namespace Acme.Center.Platform.ReportsCompliance.Application.Internal.QueryServices;

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

    public async Task<CumulativeStIndicator?> Handle(GetCumulativeStIndicatorByIdQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.FindCumulativeStIndicatorByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<HistoricalIncidentRecord>> Handle(GetAllHistoricalIncidentRecordsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllHistoricalIncidentRecordsAsync(cancellationToken);
    }

    public async Task<HistoricalIncidentRecord?> Handle(GetHistoricalIncidentRecordByIdQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.FindHistoricalIncidentRecordByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<AnnualOhsPlan>> Handle(GetAllAnnualOhsPlansQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllAnnualOhsPlansAsync(cancellationToken);
    }

    public async Task<AnnualOhsPlan?> Handle(GetAnnualOhsPlanByIdQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.FindAnnualOhsPlanByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<PredictiveIndicator>> Handle(GetAllPredictiveIndicatorsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllPredictiveIndicatorsAsync(cancellationToken);
    }

    public async Task<PredictiveIndicator?> Handle(GetPredictiveIndicatorByIdQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.FindPredictiveIndicatorByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<CriticalAlert>> Handle(GetAllCriticalAlertsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllCriticalAlertsAsync(cancellationToken);
    }

    public async Task<CriticalAlert?> Handle(GetCriticalAlertByIdQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.FindCriticalAlertByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<GeneratedReport>> Handle(GetAllGeneratedReportsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllGeneratedReportsAsync(cancellationToken);
    }

    public async Task<GeneratedReport?> Handle(GetGeneratedReportByIdQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.FindGeneratedReportByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<KpiDashboard>> Handle(GetAllKpiDashboardQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllKpiDashboardAsync(cancellationToken);
    }

    public async Task<KpiDashboard?> Handle(GetKpiDashboardByIdQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.FindKpiDashboardByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<HistoricalTrend>> Handle(GetAllHistoricalTrendsQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.GetAllHistoricalTrendsAsync(cancellationToken);
    }

    public async Task<HistoricalTrend?> Handle(GetHistoricalTrendByIdQuery query, CancellationToken cancellationToken)
    {
        return await baseReportsRepository.FindHistoricalTrendByIdAsync(query.Id, cancellationToken);
    }
}
