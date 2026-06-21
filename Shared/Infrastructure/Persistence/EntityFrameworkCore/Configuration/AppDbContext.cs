using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Iam.Domain.Model.Aggregates;
using Acme.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // ReportsCompliance
    public DbSet<MonthlyReport> MonthlyReports => Set<MonthlyReport>();
    public DbSet<CumulativeStIndicator> CumulativeStIndicators => Set<CumulativeStIndicator>();
    public DbSet<HistoricalIncidentRecord> HistoricalIncidentRecords => Set<HistoricalIncidentRecord>();
    public DbSet<AnnualOhsPlan> AnnualOhsPlans => Set<AnnualOhsPlan>();
    public DbSet<PredictiveIndicator> PredictiveIndicators => Set<PredictiveIndicator>();
    public DbSet<CriticalAlert> CriticalAlerts => Set<CriticalAlert>();
    public DbSet<GeneratedReport> GeneratedReports => Set<GeneratedReport>();
    public DbSet<KpiDashboard> KpiDashboard => Set<KpiDashboard>();
    public DbSet<HistoricalTrend> HistoricalTrends => Set<HistoricalTrend>();

    // IAM
    public DbSet<User> Users => Set<User>();

    // Hazard
    public DbSet<Hazard> Hazards => Set<Hazard>();

    // Technician
    public DbSet<Technician> Technicians => Set<Technician>();

    // RiskAssessment
    public DbSet<RiskAssessment> RiskAssessments => Set<RiskAssessment>();
    public DbSet<RiskPattern> RiskPatterns => Set<RiskPattern>();
    public DbSet<PatternAlert> PatternAlerts => Set<PatternAlert>();
    public DbSet<AreaCriticalityLevel> AreaCriticalityLevels => Set<AreaCriticalityLevel>();
    public DbSet<DailySummary> DailySummaries => Set<DailySummary>();

    // Mitigation
    public DbSet<Mitigation> Mitigations => Set<Mitigation>();
    public DbSet<CorrectiveActionTicket> CorrectiveActionTickets => Set<CorrectiveActionTicket>();
    public DbSet<MeasureVerification> MeasureVerifications => Set<MeasureVerification>();
    public DbSet<TicketHistory> TicketHistories => Set<TicketHistory>();
    public DbSet<SlaAlert> SlaAlerts => Set<SlaAlert>();
    public DbSet<CriticalNotification> CriticalNotifications => Set<CriticalNotification>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyReportsComplianceConfiguration();
        builder.ApplyIamConfiguration();
        builder.ApplyHazardConfiguration();
        builder.ApplyTechnicianConfiguration();
        builder.ApplyRiskAssessmentConfiguration();
        builder.ApplyMitigationConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}
