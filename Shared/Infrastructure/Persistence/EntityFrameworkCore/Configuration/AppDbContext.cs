using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Iam.Domain.Model.Aggregates;
using RiskGuard.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.Inspections.Domain.Model.Aggregates;
using RiskGuard.Platform.Inspections.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.Mitigation.Domain.Model.Aggregates;
using RiskGuard.Platform.Mitigation.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using RiskGuard.Platform.MonitoringDashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.OrganizationAssets.Domain.Model.Aggregates;
using RiskGuard.Platform.OrganizationAssets.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.RiskAssessment.Domain.Model.Aggregates;
using RiskGuard.Platform.RiskAssessment.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<AccessLog> AccessLogs => Set<AccessLog>();
    public DbSet<Sede> Sedes => Set<Sede>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Activo> Activos => Set<Activo>();
    public DbSet<Inspeccion> Inspecciones => Set<Inspeccion>();
    public DbSet<Peligro> Peligros => Set<Peligro>();
    public DbSet<EvidenciaFotografica> EvidenciasFotograficas => Set<EvidenciaFotografica>();
    public DbSet<EvaluacionRiesgo> EvaluacionesRiesgo => Set<EvaluacionRiesgo>();
    public DbSet<PatronRiesgo> PatronesRiesgo => Set<PatronRiesgo>();
    public DbSet<NivelCriticidadArea> NivelesCriticidadArea => Set<NivelCriticidadArea>();
    public DbSet<AlertaPatron> AlertasPatron => Set<AlertaPatron>();
    public DbSet<ResumenDiario> ResumenesDiarios => Set<ResumenDiario>();
    public DbSet<Mitigacion> Mitigaciones => Set<Mitigacion>();
    public DbSet<TicketAccionCorrectiva> TicketsAccionCorrectiva => Set<TicketAccionCorrectiva>();
    public DbSet<VerificacionMedida> VerificacionesMedida => Set<VerificacionMedida>();
    public DbSet<HistorialTicket> HistorialesTicket => Set<HistorialTicket>();
    public DbSet<AlertaSla> AlertasSla => Set<AlertaSla>();
    public DbSet<NotificacionCritica> NotificacionesCriticas => Set<NotificacionCritica>();
    public DbSet<Tecnico> Tecnicos => Set<Tecnico>();
    public DbSet<HeatMapZone> HeatMapZones => Set<HeatMapZone>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Technician> Technicians => Set<Technician>();
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<PreventiveMaintenance> PreventiveMaintenances => Set<PreventiveMaintenance>();
    public DbSet<ArchivedReport> ArchivedReports => Set<ArchivedReport>();
    public DbSet<MonthlyReport> MonthlyReports => Set<MonthlyReport>();
    public DbSet<CumulativeStIndicator> CumulativeStIndicators => Set<CumulativeStIndicator>();
    public DbSet<HistoricalIncidentRecord> HistoricalIncidentRecords => Set<HistoricalIncidentRecord>();
    public DbSet<AnnualOhsPlan> AnnualOhsPlans => Set<AnnualOhsPlan>();
    public DbSet<PredictiveIndicator> PredictiveIndicators => Set<PredictiveIndicator>();
    public DbSet<CriticalAlert> CriticalAlerts => Set<CriticalAlert>();
    public DbSet<GeneratedReport> GeneratedReports => Set<GeneratedReport>();
    public DbSet<KpiDashboard> KpiDashboard => Set<KpiDashboard>();
    public DbSet<HistoricalTrend> HistoricalTrends => Set<HistoricalTrend>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyIamConfiguration();
        builder.ApplyOrganizationAssetsConfiguration();
        builder.ApplyInspectionsConfiguration();
        builder.ApplyRiskAssessmentConfiguration();
        builder.ApplyMitigationConfiguration();
        builder.ApplyMonitoringDashboardConfiguration();
        builder.ApplyReportsComplianceConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}
