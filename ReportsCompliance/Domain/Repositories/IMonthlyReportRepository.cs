using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;

namespace RiskGuard.Platform.ReportsCompliance.Domain.Repositories;

public interface IMonthlyReportRepository : IBaseRepository<MonthlyReport>
{
    Task<IEnumerable<MonthlyReport>> ListByYearAsync(int year, CancellationToken cancellationToken = default);
}
