using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MonthlyReportRepository(AppDbContext context) : BaseRepository<MonthlyReport>(context), IMonthlyReportRepository
{
    public async Task<IEnumerable<MonthlyReport>> ListByYearAsync(int year, CancellationToken cancellationToken = default)
    {
        return await Context.MonthlyReports.Where(report => report.Year == year).ToListAsync(cancellationToken);
    }
}
