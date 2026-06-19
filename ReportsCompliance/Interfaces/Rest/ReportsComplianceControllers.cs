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
