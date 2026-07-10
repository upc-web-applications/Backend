using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;

namespace Acme.Center.Platform.ReportsCompliance.Domain.Repositories;

public interface IBaseReportsRepository
{
    Task<IEnumerable<HistoricalIncidentRecord>> GetAllHistoricalIncidentRecordsAsync(CancellationToken cancellationToken = default);
    Task<HistoricalIncidentRecord?> FindHistoricalIncidentRecordByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<AnnualOhsPlan>> GetAllAnnualOhsPlansAsync(CancellationToken cancellationToken = default);
    Task<AnnualOhsPlan?> FindAnnualOhsPlanByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PredictiveIndicator>> GetAllPredictiveIndicatorsAsync(CancellationToken cancellationToken = default);
    Task<PredictiveIndicator?> FindPredictiveIndicatorByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CriticalAlert>> GetAllCriticalAlertsAsync(CancellationToken cancellationToken = default);
    Task<CriticalAlert?> FindCriticalAlertByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<GeneratedReport>> GetAllGeneratedReportsAsync(CancellationToken cancellationToken = default);
    Task<GeneratedReport?> FindGeneratedReportByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<KpiDashboard>> GetAllKpiDashboardAsync(CancellationToken cancellationToken = default);
    Task<KpiDashboard?> FindKpiDashboardByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<HistoricalTrend>> GetAllHistoricalTrendsAsync(CancellationToken cancellationToken = default);
    Task<HistoricalTrend?> FindHistoricalTrendByIdAsync(string id, CancellationToken cancellationToken = default);
}
