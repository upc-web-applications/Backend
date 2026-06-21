using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Hazards.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Iam.Domain.Model.Aggregates;
using Acme.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Inspections.Domain.Model.Aggregates;
using Acme.Center.Platform.Inspections.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using Acme.Center.Platform.MonitoringDashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;
using Acme.Center.Platform.OrganizationAssets.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Technicians.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using MonitorAsset = Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates.Asset;
using MonitorTicket = Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates.Ticket;
using MonitorTechnician = Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates.Technician;
using OrgAsset = Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates.Asset;

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
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<AccessLog> AccessLogs => Set<AccessLog>();

    // Hazard
    public DbSet<Hazard> Hazards => Set<Hazard>();

    // Technician
    public DbSet<Acme.Center.Platform.Technicians.Domain.Model.Aggregates.Technician> Technicians
        => Set<Acme.Center.Platform.Technicians.Domain.Model.Aggregates.Technician>();

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

    // MonitoringDashboard
    public DbSet<HeatMapZone> HeatMapZones => Set<HeatMapZone>();
    public DbSet<MonitorTicket> DashboardTickets => Set<MonitorTicket>();
    public DbSet<MonitorTechnician> DashboardTechnicians => Set<MonitorTechnician>();
    public DbSet<MonitorAsset> DashboardAssets => Set<MonitorAsset>();
    public DbSet<PreventiveMaintenance> PreventiveMaintenances => Set<PreventiveMaintenance>();
    public DbSet<ArchivedReport> ArchivedReports => Set<ArchivedReport>();

    // Inspections
    public DbSet<Inspection> Inspections => Set<Inspection>();
    public DbSet<Danger> Dangers => Set<Danger>();
    public DbSet<PhotoEvidence> PhotoEvidences => Set<PhotoEvidence>();

    // OrganizationAssets
    public DbSet<Headquarters> Headquarters => Set<Headquarters>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<OrgAsset> OrgAssets => Set<OrgAsset>();

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
        builder.ApplyMonitoringDashboardConfiguration();
        builder.ApplyInspectionsConfiguration();
        builder.ApplyOrganizationAssetsConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}
