using Microsoft.AspNetCore.Mvc;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Interfaces.Rest;

namespace RiskGuard.Platform.ReportsCompliance.Interfaces.Rest;

[Route("api/v1/monthly_reports")]
public class MonthlyReportsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<MonthlyReport>(context, unitOfWork);

[Route("api/v1/cumulative_st_indicators")]
public class CumulativeStIndicatorsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<CumulativeStIndicator>(context, unitOfWork);

[Route("api/v1/historical_incident_records")]
public class HistoricalIncidentRecordsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<HistoricalIncidentRecord>(context, unitOfWork);

[Route("api/v1/annual_ohs_plan")]
public class AnnualOhsPlanController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<AnnualOhsPlan>(context, unitOfWork);

[Route("api/v1/predictive_indicators")]
public class PredictiveIndicatorsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<PredictiveIndicator>(context, unitOfWork);

[Route("api/v1/critical_alerts")]
public class CriticalAlertsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<CriticalAlert>(context, unitOfWork);
