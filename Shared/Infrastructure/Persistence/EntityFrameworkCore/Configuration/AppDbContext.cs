using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MonthlyReport> MonthlyReports => Set<MonthlyReport>();
    public DbSet<CumulativeStIndicator> CumulativeStIndicators => Set<CumulativeStIndicator>();
    public DbSet<HistoricalIncidentRecord> HistoricalIncidentRecords => Set<HistoricalIncidentRecord>();
    public DbSet<AnnualOhsPlan> AnnualOhsPlans => Set<AnnualOhsPlan>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyReportsComplianceConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}
