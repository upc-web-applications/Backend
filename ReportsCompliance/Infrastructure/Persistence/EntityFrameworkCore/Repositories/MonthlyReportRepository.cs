using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace RiskGuard.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MonthlyReportRepository(AppDbContext context) : BaseRepository<MonthlyReport>(context), IMonthlyReportRepository
{
    public async Task<IEnumerable<MonthlyReport>> ListByYearAsync(int year, CancellationToken cancellationToken = default)
    {
        return await Context.MonthlyReports.Where(report => report.Year == year).ToListAsync(cancellationToken);
    }
}
