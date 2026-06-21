using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RiskGuard.Platform.Iam.Domain.Model.Aggregates;
using RiskGuard.Platform.Inspections.Domain.Model.Aggregates;
using RiskGuard.Platform.Mitigation.Domain.Model.Aggregates;
using RiskGuard.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using RiskGuard.Platform.OrganizationAssets.Domain.Model.Aggregates;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
using RiskGuard.Platform.RiskAssessment.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Seeding;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseSeeder");
        try
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            await context.Database.EnsureCreatedAsync();
            if (await context.Roles.AnyAsync()) return;

            context.Roles.AddRange(
                new Role { Id = "c1a42619-e994-48f0-92c6-cdf0a104e7a1", Code = "plant-operator", Name = "Operario de planta", Description = "Reporta condiciones inseguras desde el sector asignado." },
                new Role { Id = "4afbd60b-8da6-46a6-9b48-2d04b8fa2161", Code = "supervisor", Name = "Supervisor", Description = "Gestiona sectores, activos y mitigacion." },
                new Role { Id = "af74b6d7-8217-44d6-b8d1-9bd5cd7555d2", Code = "administrator", Name = "Administrador", Description = "Gestiona cuentas, reportes y dashboard ejecutivo." }
            );
            context.Users.AddRange(
                new User
                {
                    Id = "operario_001", Name = "Operario Demo", Email = "operario@riskguard.tech",
                    Role = "Operario", RoleId = "c1a42619-e994-48f0-92c6-cdf0a104e7a1", SectorId = 2,
                    PasswordHash = "Risk123", SiteAreaId = "1"
                },
                new User
                {
                    Id = "supervisor_001", Name = "Supervisor Demo", Email = "supervisor@riskguard.tech",
                    Role = "Supervisor", RoleId = "4afbd60b-8da6-46a6-9b48-2d04b8fa2161", SectorId = 2,
                    PasswordHash = "Risk123", SiteAreaId = "1"
                },
                new User
                {
                    Id = "administrador_001", Name = "Administrador Demo", Email = "administrador@riskguard.tech",
                    Role = "Administrador", RoleId = "af74b6d7-8217-44d6-b8d1-9bd5cd7555d2",
                    PasswordHash = "Risk123"
                }
            );
            context.Sedes.AddRange(
                new Sede { Id = 1, Nombre = "Planta Lima Norte", Direccion = "Av. Industrial 1234", Telefono = "01-5231456", Email = "limanorte@riskguard.pe" },
                new Sede { Id = 2, Nombre = "Planta Callao", Direccion = "Jr. Manufactura 567", Telefono = "01-4298731", Email = "callao@riskguard.pe" }
            );
            context.Areas.AddRange(
                new Area { Id = 1, Nombre = "Zona de Produccion A", Codigo = "PROD-A", Descripcion = "Area principal de produccion", SedeId = 1, NivelRiesgo = "Alto" },
                new Area { Id = 2, Nombre = "Zona de Forjado", Codigo = "FORG-01", Descripcion = "Area de forjado caliente", SedeId = 1, NivelRiesgo = "Critico" },
                new Area { Id = 3, Nombre = "Almacen de Insumos", Codigo = "ALM-01", Descripcion = "Almacen operativo", SedeId = 2, NivelRiesgo = "Medio" }
            );
            context.Activos.AddRange(
                new Activo { Id = 1, Nombre = "Prensa 500T", Codigo = "PRS-500", NumeroSerie = "P500-001", AreaId = 2, SedeId = 1, Descripcion = "Prensa hidraulica" },
                new Activo { Id = 2, Nombre = "Montacargas H2", Codigo = "MNT-H2", NumeroSerie = "MNT-002", AreaId = 3, SedeId = 2, Descripcion = "Montacargas de almacen" }
            );
            context.Peligros.AddRange(
                new Peligro { Id = 1, Nombre = "Condicion insegura", Categoria = "Fisico", Descripcion = "Piso, maquinaria o ambiente inseguro" },
                new Peligro { Id = 2, Nombre = "Falla de equipo", Categoria = "Mecanico", Descripcion = "Falla operativa de activo industrial" }
            );
            context.Inspecciones.Add(new Inspeccion
            {
                Id = 1, Ticket = "TICK-4821", TipoIncidente = "Condicion insegura", AreaId = 2, SedeId = 1,
                ActivoId = 1, NivelUrgencia = "Alto", Descripcion = "Fuga de aceite hidraulico en la base de la prensa.",
                Estado = "Pendiente", OperarioId = "operario_001"
            });
            context.EvaluacionesRiesgo.Add(new EvaluacionRiesgo
            {
                Id = 1, InspeccionId = 1, AreaId = 2, ProbabilityIndex = 4, SeverityIndex = 5,
                NivelRiesgoPuro = 20, ClasificacionRiesgo = "Critico", EsTolerable = false
            });
            context.NivelesCriticidadArea.Add(new NivelCriticidadArea
                { Id = 1, AreaId = 2, PuntajeCriticidadActual = 20, NivelRiesgoVisual = "Critico" });
            context.PatronesRiesgo.Add(new PatronRiesgo
            {
                Id = 1, AreaId = 2, ActivoId = 1, TipoRiesgo = "Fisico",
                DescripcionPatron = "Fugas recurrentes en prensa hidraulica", CantidadOcurrencias = 4
            });
            context.AlertasPatron.Add(new AlertaPatron
                { Id = 1, AreaId = 2, PatronRiesgoId = 1, MensajeAlerta = "Patron recurrente detectado", NivelUrgencia = "Alta" });
            context.ResumenesDiarios.Add(new ResumenDiario { Id = 1, AreaId = 2, Nuevos = 2, EnProgreso = 1, Resueltos = 0 });
            context.Tecnicos.Add(new Tecnico
                { Id = 1, DocumentoIdentidad = "71234567", NombreCompleto = "Juan Perez Lopez", Especialidad = "Mecanica", EstadoOperativo = "Activo" });
            context.Mitigaciones.Add(new Mitigacion
            {
                Id = 1, RiskAssessmentId = 1, TicketId = 1, Codigo = "MIT-2026-001",
                Descripcion = "Instalacion de barreras termicas alrededor del horno de forjado",
                Responsable = "Juan Perez - Mto. Industrial",
                FechaAsignacion = DateTime.UtcNow.Date,
                Estado = "Asignado",
                Resultado = "Pendiente"
            });
            context.TicketsAccionCorrectiva.Add(new TicketAccionCorrectiva
            {
                Id = 1, ReporteId = 1, SectorId = 2, Sector = "Zona de Forjado", TipoRiesgo = "Fisico",
                NivelCriticidad = "Critico", Estado = "Pendiente", Instrucciones = "Aislar zona y reparar fuga",
                SlaLimiteHoras = 24
            });
            context.HeatMapZones.Add(new HeatMapZone { Id = 1, Name = "Zona de Forjado", SectorId = 2, HeatIndex = 95, RiskLevel = "Critico" });
            context.Tickets.Add(new Ticket { Id = 1, SectorId = 2, Title = "Fuga de aceite en prensa", Status = "Pendiente", Priority = "Alta" });
            context.Technicians.Add(new Technician { Id = "tech_001", Name = "Juan Perez Lopez", Specialty = "Mecanica", Status = "Activo" });
            context.Assets.Add(new Asset { Id = "asset_001", Name = "Prensa 500T", Code = "PRS-500", SectorId = 2, Status = "Activo" });
            context.PreventiveMaintenances.Add(new PreventiveMaintenance { Id = "pm_001", AssetId = "asset_001", Description = "Revision hidraulica mensual" });
            context.ArchivedReports.Add(new ArchivedReport { Id = "ar_001", Title = "Reporte SST inicial", Url = "/reports/sst-inicial.pdf", HashIntegrity = "demo-hash" });
            context.MonthlyReports.Add(new MonthlyReport { Id = "MR_2026_05", Month = 5, Year = 2026 });
            context.CumulativeStIndicators.Add(new CumulativeStIndicator { Id = "CSI_001", Name = "resolution_rate", Value = 72, Status = "acceptable" });
            context.HistoricalIncidentRecords.Add(new HistoricalIncidentRecord { Id = "HIR_001", Sector = "Zona de Forjado", IncidentType = "Condicion insegura", Criticality = "Critico" });
            context.AnnualOhsPlans.Add(new AnnualOhsPlan { Id = "PLAN_2026", Year = 2026, GlobalCompliance = 72, Goal = 80, CompletedActivities = 86, TotalActivities = 120 });
            context.PredictiveIndicators.Add(new PredictiveIndicator { Id = "PI_001", Name = "Sectores con tendencia creciente", Description = "Zona de Forjado en incremento", Value = 1, Trend = "up" });
            context.CriticalAlerts.Add(new CriticalAlert { Id = "CA_001", Type = "critical", Sector = "Zona de Forjado", RiskType = "Fisico", Message = "Riesgo critico sin resolver", ElapsedHours = 26 });
            context.GeneratedReports.Add(new GeneratedReport { Id = "GR_001", Type = "compliance", Year = 2026, Format = "pdf", FileName = "RiskGuard_Cumplimiento_2026.pdf" });
            context.KpiDashboard.AddRange(
                new KpiDashboard { Id = "KPI_001", Name = "active_incidents", Value = 1, Goal = 0, Status = "alert" },
                new KpiDashboard { Id = "KPI_002", Name = "ohs_compliance", Value = 72, Goal = 80, Status = "warning" }
            );
            context.HistoricalTrends.Add(new HistoricalTrend { Id = "HT_001", Month = 5, Year = 2026, TotalIncidents = 4, Sector = "Zona de Forjado", Type = "Condicion insegura" });

            await context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "RiskGuard database seeding was skipped. Check MySQL availability and connection string.");
        }
    }
}
