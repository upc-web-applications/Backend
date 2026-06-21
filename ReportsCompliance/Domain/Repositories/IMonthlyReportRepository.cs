using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.ReportsCompliance.Domain.Repositories;

public interface IMonthlyReportRepository : IBaseRepository<MonthlyReport>
{
    Task<IEnumerable<MonthlyReport>> ListByYearAsync(int year, CancellationToken cancellationToken = default);
}
