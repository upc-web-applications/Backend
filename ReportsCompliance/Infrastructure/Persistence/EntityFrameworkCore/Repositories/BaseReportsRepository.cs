using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class BaseReportsRepository(AppDbContext context) : IBaseReportsRepository
{
    public async Task<IEnumerable<CumulativeStIndicator>> GetAllCumulativeStIndicatorsAsync(CancellationToken cancellationToken = default)
    {
        return await context.CumulativeStIndicators.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<CumulativeStIndicator?> FindCumulativeStIndicatorByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.CumulativeStIndicators.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<HistoricalIncidentRecord>> GetAllHistoricalIncidentRecordsAsync(CancellationToken cancellationToken = default)
    {
        return await context.HistoricalIncidentRecords.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<HistoricalIncidentRecord?> FindHistoricalIncidentRecordByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.HistoricalIncidentRecords.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<AnnualOhsPlan>> GetAllAnnualOhsPlansAsync(CancellationToken cancellationToken = default)
    {
        return await context.AnnualOhsPlans.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<AnnualOhsPlan?> FindAnnualOhsPlanByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.AnnualOhsPlans.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<PredictiveIndicator>> GetAllPredictiveIndicatorsAsync(CancellationToken cancellationToken = default)
    {
        return await context.PredictiveIndicators.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PredictiveIndicator?> FindPredictiveIndicatorByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.PredictiveIndicators.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<CriticalAlert>> GetAllCriticalAlertsAsync(CancellationToken cancellationToken = default)
    {
        return await context.CriticalAlerts.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<CriticalAlert?> FindCriticalAlertByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.CriticalAlerts.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<GeneratedReport>> GetAllGeneratedReportsAsync(CancellationToken cancellationToken = default)
    {
        return await context.GeneratedReports.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<GeneratedReport?> FindGeneratedReportByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.GeneratedReports.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<KpiDashboard>> GetAllKpiDashboardAsync(CancellationToken cancellationToken = default)
    {
        return await context.KpiDashboard.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<KpiDashboard?> FindKpiDashboardByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.KpiDashboard.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<HistoricalTrend>> GetAllHistoricalTrendsAsync(CancellationToken cancellationToken = default)
    {
        return await context.HistoricalTrends.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<HistoricalTrend?> FindHistoricalTrendByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await context.HistoricalTrends.FindAsync([id], cancellationToken);
    }
}
