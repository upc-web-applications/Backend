using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;

namespace RiskGuard.Platform.ReportsCompliance.Domain.Repositories;

public interface IBaseReportsRepository
{
    Task<IEnumerable<CumulativeStIndicator>> GetAllCumulativeStIndicatorsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<HistoricalIncidentRecord>> GetAllHistoricalIncidentRecordsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AnnualOhsPlan>> GetAllAnnualOhsPlansAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PredictiveIndicator>> GetAllPredictiveIndicatorsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<CriticalAlert>> GetAllCriticalAlertsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<GeneratedReport>> GetAllGeneratedReportsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<KpiDashboard>> GetAllKpiDashboardAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<HistoricalTrend>> GetAllHistoricalTrendsAsync(CancellationToken cancellationToken = default);
}
