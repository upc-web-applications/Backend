using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Queries;

namespace Acme.Center.Platform.ReportsCompliance.Application.QueryServices;

public interface IReportsComplianceQueryService
{
    Task<IEnumerable<HistoricalIncidentRecord>> Handle(GetAllHistoricalIncidentRecordsQuery query, CancellationToken cancellationToken);
    Task<HistoricalIncidentRecord?> Handle(GetHistoricalIncidentRecordByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<AnnualOhsPlan>> Handle(GetAllAnnualOhsPlansQuery query, CancellationToken cancellationToken);
    Task<AnnualOhsPlan?> Handle(GetAnnualOhsPlanByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<PredictiveIndicator>> Handle(GetAllPredictiveIndicatorsQuery query, CancellationToken cancellationToken);
    Task<PredictiveIndicator?> Handle(GetPredictiveIndicatorByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<CriticalAlert>> Handle(GetAllCriticalAlertsQuery query, CancellationToken cancellationToken);
    Task<CriticalAlert?> Handle(GetCriticalAlertByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<GeneratedReport>> Handle(GetAllGeneratedReportsQuery query, CancellationToken cancellationToken);
    Task<GeneratedReport?> Handle(GetGeneratedReportByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<KpiDashboard>> Handle(GetAllKpiDashboardQuery query, CancellationToken cancellationToken);
    Task<KpiDashboard?> Handle(GetKpiDashboardByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<HistoricalTrend>> Handle(GetAllHistoricalTrendsQuery query, CancellationToken cancellationToken);
    Task<HistoricalTrend?> Handle(GetHistoricalTrendByIdQuery query, CancellationToken cancellationToken);
}
