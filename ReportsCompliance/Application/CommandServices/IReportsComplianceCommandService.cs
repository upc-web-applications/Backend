using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.ReportsCompliance.Application.CommandServices;

public interface IReportsComplianceCommandService
{
    Task<Result<MonthlyReport>> Handle(CreateMonthlyReportCommand command, CancellationToken cancellationToken);
    Task<Result<CumulativeStIndicator>> Handle(CreateCumulativeStIndicatorCommand command, CancellationToken cancellationToken);
    Task<Result<HistoricalIncidentRecord>> Handle(CreateHistoricalIncidentRecordCommand command, CancellationToken cancellationToken);
    Task<Result<AnnualOhsPlan>> Handle(CreateAnnualOhsPlanCommand command, CancellationToken cancellationToken);
    Task<Result<PredictiveIndicator>> Handle(CreatePredictiveIndicatorCommand command, CancellationToken cancellationToken);
    Task<Result<CriticalAlert>> Handle(CreateCriticalAlertCommand command, CancellationToken cancellationToken);
    Task<Result<GeneratedReport>> Handle(CreateGeneratedReportCommand command, CancellationToken cancellationToken);
    Task<Result<KpiDashboard>> Handle(CreateKpiDashboardCommand command, CancellationToken cancellationToken);
    Task<Result<HistoricalTrend>> Handle(CreateHistoricalTrendCommand command, CancellationToken cancellationToken);
}
