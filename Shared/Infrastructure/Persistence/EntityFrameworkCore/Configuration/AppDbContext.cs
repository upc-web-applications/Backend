using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;

namespace RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MonthlyReport> MonthlyReports => Set<MonthlyReport>();
    public DbSet<CumulativeStIndicator> CumulativeStIndicators => Set<CumulativeStIndicator>();
    public DbSet<HistoricalIncidentRecord> HistoricalIncidentRecords => Set<HistoricalIncidentRecord>();
    public DbSet<AnnualOhsPlan> AnnualOhsPlans => Set<AnnualOhsPlan>();
    public DbSet<PredictiveIndicator> PredictiveIndicators => Set<PredictiveIndicator>();
    public DbSet<CriticalAlert> CriticalAlerts => Set<CriticalAlert>();
    public DbSet<GeneratedReport> GeneratedReports => Set<GeneratedReport>();
    public DbSet<KpiDashboard> KpiDashboard => Set<KpiDashboard>();
    public DbSet<HistoricalTrend> HistoricalTrends => Set<HistoricalTrend>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyReportsComplianceConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}
