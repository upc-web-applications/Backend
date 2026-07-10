using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Iam.Domain.Model.Aggregates;
using Acme.Center.Platform.Mitigations.Domain.Model.Aggregates;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using OrgAsset = Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates.Asset;
using MonitorTechnician = Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates.Technician;
using MonitorAsset = Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates.Asset;
using MonitorTicket = Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates.Ticket;
using Acme.Center.Platform.Inspections.Domain.Model.Aggregates;
using Acme.Center.Platform.MonitoringDashboard.Domain.Model.Aggregates;
using Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Seeding;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseSeeder");
        try
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            await context.Database.EnsureCreatedAsync();

            // ── IAM: Roles ──
            var roleAdmin = await context.Roles.FindAsync("ROLE_ADMIN");
            var roleSupervisor = await context.Roles.FindAsync("ROLE_SUPER");
            var roleOperator = await context.Roles.FindAsync("ROLE_OPER");

            if (!await context.Roles.AnyAsync())
            {
                roleAdmin = new Role { Id = "ROLE_ADMIN", Code = "administrator", Name = "Administrator", Description = "Full system access" };
                roleSupervisor = new Role { Id = "ROLE_SUPER", Code = "supervisor", Name = "Supervisor", Description = "Operational supervision" };
                roleOperator = new Role { Id = "ROLE_OPER", Code = "plant-operator", Name = "Plant Operator", Description = "Inspection and reporting" };
                context.Roles.AddRange(roleAdmin, roleSupervisor, roleOperator);
                await context.SaveChangesAsync();
            }

            // ── IAM: Demo users ──
            var supervisorUser = await context.Users.FirstOrDefaultAsync(u => u.Username == "supervisor");
            var operarioUser = await context.Users.FirstOrDefaultAsync(u => u.Username == "operario");
            var operarioUser2 = await context.Users.FirstOrDefaultAsync(u => u.Username == "operario2");

            if (!await context.Users.AnyAsync())
            {
                var adminUser = new User("admin", BCryptNet.HashPassword("Risk123"))
                {
                    Email = "admin@riskguard.tech",
                    Name = "Admin RiskGuard",
                    Role = "Administrator",
                    RoleId = roleAdmin!.Id,
                    AccountStatus = "ACTIVE"
                };
                supervisorUser = new User("supervisor", BCryptNet.HashPassword("Risk123"))
                {
                    Email = "supervisor@riskguard.tech",
                    Name = "Supervisor RiskGuard",
                    Role = "Supervisor",
                    RoleId = roleSupervisor!.Id,
                    AccountStatus = "ACTIVE"
                };
                var supervisorUser2 = new User("supervisor2", BCryptNet.HashPassword("Risk123"))
                {
                    Email = "supervisor2@riskguard.tech",
                    Name = "Supervisor Dos",
                    Role = "Supervisor",
                    RoleId = roleSupervisor.Id,
                    AccountStatus = "ACTIVE"
                };
                operarioUser = new User("operario", BCryptNet.HashPassword("Risk123"))
                {
                    Email = "operario@riskguard.tech",
                    Name = "Operario RiskGuard",
                    Role = "Operator",
                    RoleId = roleOperator!.Id,
                    AccountStatus = "ACTIVE"
                };
                operarioUser2 = new User("operario2", BCryptNet.HashPassword("Risk123"))
                {
                    Email = "operario2@riskguard.tech",
                    Name = "Operario Dos",
                    Role = "Operator",
                    RoleId = roleOperator.Id,
                    AccountStatus = "ACTIVE"
                };
                context.Users.AddRange(adminUser, supervisorUser, supervisorUser2, operarioUser, operarioUser2);
                await context.SaveChangesAsync();
            }

            // ── IAM: Sessions ──
            if (!await context.Sessions.AnyAsync())
            {
                context.Sessions.Add(new Session
                {
                    Id = "SES_001",
                    UserId = supervisorUser!.Id,
                    TokenSignature = "seed-session-token-supervisor",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    LastActivityAt = DateTime.UtcNow.AddHours(-2),
                    IsValid = true
                });
                await context.SaveChangesAsync();
            }

            // ── IAM: Access Logs ──
            if (!await context.AccessLogs.AnyAsync())
            {
                context.AccessLogs.Add(new AccessLog
                {
                    Id = "ALOG_001",
                    UserId = supervisorUser!.Id,
                    Email = "supervisor@riskguard.tech",
                    AttemptAt = DateTime.UtcNow.AddDays(-1),
                    WasSuccessful = true,
                    IpAddress = "192.168.1.100"
                });
                await context.SaveChangesAsync();
            }

            // ── OrganizationAssets: Headquarters ──
            if (!await context.Headquarters.AnyAsync())
            {
                context.Headquarters.AddRange(
                    new Headquarters { Id = 1, Name = "Planta Central", Address = "Av. Industrial 1500, Lima", Phone = "511-5550101", Email = "central@riskguard.tech", Status = "Active" },
                    new Headquarters { Id = 2, Name = "Almacén Norte", Address = "Panamericana Norte Km 25, Lima", Phone = "511-5550102", Email = "almacen.norte@riskguard.tech", Status = "Active" },
                    new Headquarters { Id = 3, Name = "Talleres Sur", Address = "Av. Los Talleres 500, Ica", Phone = "511-5550103", Email = "talleres.sur@riskguard.tech", Status = "Active" },
                    new Headquarters { Id = 4, Name = "Planta Costera", Address = "Carretera Costa 800, Callao", Phone = "511-5550104", Email = "costera@riskguard.tech", Status = "Active" }
                );
                await context.SaveChangesAsync();
            }

            // ── OrganizationAssets: Areas ──
            if (!await context.Areas.AnyAsync())
            {
                context.Areas.AddRange(
                    new Area { Id = 1, Name = "Zona de Forjado", Code = "FOR-001", Description = "Area de forjado de metales", HeadquartersId = 1, Status = "Active", RiskLevel = "High" },
                    new Area { Id = 2, Name = "Almacen de Quimicos", Code = "AQM-001", Description = "Almacenamiento de sustancias quimicas", HeadquartersId = 1, Status = "Active", RiskLevel = "Critical" },
                    new Area { Id = 3, Name = "Linea de Ensamblaje", Code = "ENS-001", Description = "Linea principal de ensamblaje", HeadquartersId = 1, Status = "Active", RiskLevel = "Medium" },
                    new Area { Id = 4, Name = "Zona de Soldadura", Code = "SLD-001", Description = "Area de soldadura automatizada", HeadquartersId = 2, Status = "Active", RiskLevel = "High" },
                    new Area { Id = 5, Name = "Almacen de Repuestos", Code = "RPT-001", Description = "Almacen de repuestos y consumibles", HeadquartersId = 2, Status = "Active", RiskLevel = "Low" },
                    new Area { Id = 6, Name = "Taller de Mantenimiento", Code = "MTN-001", Description = "Taller de mantenimiento general", HeadquartersId = 3, Status = "Active", RiskLevel = "Medium" },
                    new Area { Id = 7, Name = "Zona de Pintura", Code = "PNT-001", Description = "Cabina de pintura industrial", HeadquartersId = 4, Status = "Active", RiskLevel = "Medium" },
                    new Area { Id = 8, Name = "Patio de Almacenamiento", Code = "PAT-001", Description = "Patio exterior de almacenamiento", HeadquartersId = 4, Status = "Inactive", RiskLevel = "Low" }
                );
                await context.SaveChangesAsync();
            }

            // ── OrganizationAssets: Assets ──
            if (!await context.OrgAssets.AnyAsync())
            {
                context.OrgAssets.AddRange(
                    new OrgAsset { Id = 1, Name = "Prensa Hidraulica #3", Code = "PH-003", SerialNumber = "SER-PH-2024-001", Description = "Prensa hidraulica 500 ton", AreaId = 1, HeadquartersId = 1, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-6), AcquisitionDate = DateTime.UtcNow.AddMonths(-6), LastMaintenanceDate = DateTime.UtcNow.AddDays(-15) },
                    new OrgAsset { Id = 2, Name = "Tanque Solvente TQ-05", Code = "TQ-05", SerialNumber = "SER-TQ-2023-012", Description = "Tanque de almacenamiento de solventes", AreaId = 2, HeadquartersId = 1, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-12), AcquisitionDate = DateTime.UtcNow.AddMonths(-12), LastMaintenanceDate = DateTime.UtcNow.AddDays(-30) },
                    new OrgAsset { Id = 3, Name = "Robot Soldador RS-200", Code = "RS-200", SerialNumber = "SER-RS-2024-008", Description = "Robot de soldadura industrial", AreaId = 4, HeadquartersId = 2, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-3), AcquisitionDate = DateTime.UtcNow.AddMonths(-3), LastMaintenanceDate = DateTime.UtcNow.AddDays(-5) },
                    new OrgAsset { Id = 4, Name = "Cinta Transportadora CT-01", Code = "CT-01", SerialNumber = "SER-CT-2023-045", Description = "Cinta transportadora principal", AreaId = 3, HeadquartersId = 1, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-18), AcquisitionDate = DateTime.UtcNow.AddMonths(-18), LastMaintenanceDate = DateTime.UtcNow.AddDays(-10) },
                    new OrgAsset { Id = 5, Name = "Compresor de Aire CA-100", Code = "CA-100", SerialNumber = "SER-CA-2024-003", Description = "Compresor de aire industrial", AreaId = 6, HeadquartersId = 3, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-2), AcquisitionDate = DateTime.UtcNow.AddMonths(-2), LastMaintenanceDate = DateTime.UtcNow.AddDays(-3) },
                    new OrgAsset { Id = 6, Name = "Extractor de Gases EG-01", Code = "EG-01", SerialNumber = "SER-EG-2023-022", Description = "Sistema de extraccion de gases", AreaId = 2, HeadquartersId = 1, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-9), AcquisitionDate = DateTime.UtcNow.AddMonths(-9), LastMaintenanceDate = DateTime.UtcNow.AddDays(-20) },
                    new OrgAsset { Id = 7, Name = "Puente Grua PG-10T", Code = "PG-10T", SerialNumber = "SER-PG-2024-006", Description = "Puente grua 10 toneladas", AreaId = 1, HeadquartersId = 1, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-1), AcquisitionDate = DateTime.UtcNow.AddMonths(-1), LastMaintenanceDate = DateTime.UtcNow.AddDays(-2) },
                    new OrgAsset { Id = 8, Name = "Sierra Sin Fin SSF-01", Code = "SSF-01", SerialNumber = "SER-SSF-2023-031", Description = "Sierra sin fin para corte de metales", AreaId = 4, HeadquartersId = 2, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-15), AcquisitionDate = DateTime.UtcNow.AddMonths(-15), LastMaintenanceDate = DateTime.UtcNow.AddDays(-25) },
                    new OrgAsset { Id = 9, Name = "Cabina de Pintura CP-01", Code = "CP-01", SerialNumber = "SER-CP-2024-011", Description = "Cabina de pintura electroestatica", AreaId = 7, HeadquartersId = 4, Status = "Active", SystemEntryDate = DateTime.UtcNow.AddMonths(-4), AcquisitionDate = DateTime.UtcNow.AddMonths(-4), LastMaintenanceDate = DateTime.UtcNow.AddDays(-7) },
                    new OrgAsset { Id = 10, Name = "Montacargas MC-50", Code = "MC-50", SerialNumber = "SER-MC-2023-019", Description = "Montacargas industrial 5 ton", AreaId = 8, HeadquartersId = 4, Status = "Inactive", SystemEntryDate = DateTime.UtcNow.AddMonths(-20), AcquisitionDate = DateTime.UtcNow.AddMonths(-20), LastMaintenanceDate = DateTime.UtcNow.AddMonths(-2) }
                );
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: HistoricalIncidentRecords ──
            if (!await context.HistoricalIncidentRecords.AnyAsync())
            {
                context.HistoricalIncidentRecords.AddRange(
                    new HistoricalIncidentRecord { Id = "HIR_001", Sector = "Zona de Forjado", IncidentType = "Condicion insegura", Criticality = "Critico", IncidentDate = DateTime.UtcNow.AddDays(-10), Description = "Acumulacion de material inflamable cerca de fuente de calor", Resolved = true, ClosingDate = DateTime.UtcNow.AddDays(-7), ResolutionTimeHours = 72, OperatorId = operarioUser!.Id },
                    new HistoricalIncidentRecord { Id = "HIR_002", Sector = "Almacen de Quimicos", IncidentType = "Derrame", Criticality = "Critico", IncidentDate = DateTime.UtcNow.AddDays(-6), Description = "Derrame de solvente organico sin contención", Resolved = false, ClosingDate = null, ResolutionTimeHours = null, OperatorId = operarioUser!.Id },
                    new HistoricalIncidentRecord { Id = "HIR_003", Sector = "Zona de Soldadura", IncidentType = "Practica insegura", Criticality = "Alto", IncidentDate = DateTime.UtcNow.AddDays(-3), Description = "Soldadura sin pantalla de proteccion en zona comun", Resolved = false, ClosingDate = null, ResolutionTimeHours = null, OperatorId = operarioUser!.Id }
                );
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: AnnualOhsPlan ──
            if (!await context.AnnualOhsPlans.AnyAsync())
            {
                context.AnnualOhsPlans.Add(new AnnualOhsPlan { Id = "PLAN_2026", Year = 2026, GlobalCompliance = 72, Goal = 80, CompletedActivities = 86, TotalActivities = 120 });
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: PredictiveIndicators ──
            if (!await context.PredictiveIndicators.AnyAsync())
            {
                context.PredictiveIndicators.Add(new PredictiveIndicator { Id = "PI_001", Name = "Sectores con tendencia creciente", Description = "Zona de Forjado en incremento", Value = 1, Trend = "up" });
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: CriticalAlerts ──
            if (!await context.CriticalAlerts.AnyAsync())
            {
                context.CriticalAlerts.Add(new CriticalAlert { Id = "CA_001", Type = "critical", Sector = "Zona de Forjado", RiskType = "Fisico", Message = "Riesgo critico sin resolver en Zona de Forjado", ElapsedHours = 26, Status = "active", ResponsibleSupervisor = "Supervisor RiskGuard" });
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: GeneratedReports ──
            if (!await context.GeneratedReports.AnyAsync())
            {
                context.GeneratedReports.AddRange(
                    new GeneratedReport { Id = "GR_001", Type = "compliance", Year = 2026, Format = "pdf", FileName = "RiskGuard_Cumplimiento_2026.pdf", Status = "completed", GenerationDate = DateTime.UtcNow.AddDays(-5) },
                    new GeneratedReport { Id = "GR_002", Type = "incidents", Year = 2026, Format = "pdf", FileName = "RiskGuard_Incidentes_2026.pdf", Status = "completed", GenerationDate = DateTime.UtcNow.AddDays(-3) }
                );
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: KpiDashboard ──
            if (!await context.KpiDashboard.AnyAsync())
            {
                context.KpiDashboard.AddRange(
                    new KpiDashboard { Id = "KPI_001", Name = "active_incidents", Value = 1, Goal = 0, Status = "alert", UpdateDate = DateTime.UtcNow },
                    new KpiDashboard { Id = "KPI_002", Name = "ohs_compliance", Value = 72, Goal = 80, Status = "warning", UpdateDate = DateTime.UtcNow }
                );
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: HistoricalTrends ──
            if (!await context.HistoricalTrends.AnyAsync())
            {
                context.HistoricalTrends.Add(new HistoricalTrend { Id = "HT_001", Month = 5, Year = 2026, TotalIncidents = 4, Sector = "Zona de Forjado", Type = "Condicion insegura" });
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: MonthlyReports ──
            if (!await context.MonthlyReports.AnyAsync())
            {
                context.MonthlyReports.AddRange(
                    new MonthlyReport { Id = "MR_2026_05", Month = 5, Year = 2026, TotalIncidents = 12, ResolvedIncidents = 8, CompliancePercentage = 66.7m, Status = "completed", GeneratedAt = DateTime.UtcNow.AddDays(-5) },
                    new MonthlyReport { Id = "MR_2026_06", Month = 6, Year = 2026, TotalIncidents = 8, ResolvedIncidents = 5, CompliancePercentage = 62.5m, Status = "completed", GeneratedAt = DateTime.UtcNow.AddDays(-2) },
                    new MonthlyReport { Id = "MR_2026_07", Month = 7, Year = 2026, TotalIncidents = 6, ResolvedIncidents = 4, CompliancePercentage = 66.7m, Status = "completed", GeneratedAt = DateTime.UtcNow.AddDays(-1) }
                );
                await context.SaveChangesAsync();
            }

            // ── ReportsCompliance: CumulativeStIndicators ──
            if (!await context.CumulativeStIndicators.AnyAsync())
            {
                context.CumulativeStIndicators.AddRange(
                    new CumulativeStIndicator { Id = "CSI_2026_Q1", Name = "Q1 2026", Description = "Cumulative safety indicators Q1 2026", TotalIncidents = 35, ResolvedIncidents = 30, ComplianceRate = 85.7m, Period = "Q1-2026" },
                    new CumulativeStIndicator { Id = "CSI_2026_Q2", Name = "Q2 2026", Description = "Cumulative safety indicators Q2 2026", TotalIncidents = 20, ResolvedIncidents = 13, ComplianceRate = 65.0m, Period = "Q2-2026" },
                    new CumulativeStIndicator { Id = "CSI_2026_Q3", Name = "Q3 2026", Description = "Cumulative safety indicators Q3 2026", TotalIncidents = 14, ResolvedIncidents = 9, ComplianceRate = 64.3m, Period = "Q3-2026" }
                );
                await context.SaveChangesAsync();
            }

            // ── Hazard ──
            if (!await context.Hazards.AnyAsync())
            {
                context.Hazards.AddRange(
                    new Hazard { Id = "HAZ_001", Code = "FIS-001", Name = "Exposicion a ruido", Description = "Ruido continuo >85dB en zona de produccion", Category = "Fisico", BaseRiskLevel = "Medium", Status = "Active" },
                    new Hazard { Id = "HAZ_002", Code = "QUI-001", Name = "Derrame de solventes", Description = "Manipulacion de solventes organicos", Category = "Quimico", BaseRiskLevel = "High", Status = "Active" },
                    new Hazard { Id = "HAZ_003", Code = "ERG-001", Name = "Posturas forzadas", Description = "Levantamiento manual de cargas pesadas", Category = "Ergonomico", BaseRiskLevel = "Medium", Status = "Active" },
                    new Hazard { Id = "HAZ_004", Code = "FIS-002", Name = "Temperaturas extremas", Description = "Exposicion a altas temperaturas en zona de forjado", Category = "Fisico", BaseRiskLevel = "High", Status = "Active" }
                );
                await context.SaveChangesAsync();
            }

            // ── Technician ──
            if (!await context.Technicians.AnyAsync())
            {
                context.Technicians.AddRange(
                    new Acme.Center.Platform.Technicians.Domain.Model.Aggregates.Technician { Id = "TEC_001", DocumentNumber = "DNI12345678", FullName = "Carlos Mendoza Lopez", Specialty = "Electricidad", Phone = "999111222", Email = "carlos.m@riskguard.tech", Status = "Active" },
                    new Acme.Center.Platform.Technicians.Domain.Model.Aggregates.Technician { Id = "TEC_002", DocumentNumber = "DNI87654321", FullName = "Maria Garcia Torres", Specialty = "Mecanica", Phone = "999333444", Email = "maria.g@riskguard.tech", Status = "Active" },
                    new Acme.Center.Platform.Technicians.Domain.Model.Aggregates.Technician { Id = "TEC_003", DocumentNumber = "DNI45678901", FullName = "Juan Perez Ramirez", Specialty = "Soldadura", Phone = "999555666", Email = "juan.p@riskguard.tech", Status = "Active" },
                    new Acme.Center.Platform.Technicians.Domain.Model.Aggregates.Technician { Id = "TEC_004", DocumentNumber = "DNI11223344", FullName = "Lucia Fernandez Diaz", Specialty = "Pintura", Phone = "999777888", Email = "lucia.f@riskguard.tech", Status = "Active" }
                );
                await context.SaveChangesAsync();
            }

            // ── RiskAssessment ──
            if (!await context.RiskAssessments.AnyAsync())
            {
                context.RiskAssessments.AddRange(
                    new RiskAssessment { Id = "RA_001", Code = "IPERC-001", Sector = "Zona de Forjado", HazardType = "Fisico", Description = "Evaluacion de ruido en forjado", Probability = 3, Severity = 3, RiskLevel = "High", ControlMeasures = "Usar EPP auditivo", Status = "Completed", EvaluationDate = DateTime.UtcNow.AddDays(-5) },
                    new RiskAssessment { Id = "RA_002", Code = "IPERC-002", Sector = "Almacen de Quimicos", HazardType = "Quimico", Description = "Evaluacion de derrame de solventes", Probability = 2, Severity = 4, RiskLevel = "High", ControlMeasures = "Duchas de emergencia", Status = "Pending", EvaluationDate = DateTime.UtcNow.AddDays(-2) },
                    new RiskAssessment { Id = "RA_003", Code = "IPERC-003", Sector = "Linea de Ensamblaje", HazardType = "Ergonomico", Description = "Evaluacion de posturas forzadas en ensamblaje", Probability = 3, Severity = 2, RiskLevel = "Medium", ControlMeasures = "Capacitacion en pausas activas", Status = "InProgress", EvaluationDate = DateTime.UtcNow.AddDays(-1) }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.RiskPatterns.AnyAsync())
            {
                context.RiskPatterns.Add(new RiskPattern { Id = "RP_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", IncidentType = "Condicion insegura", HazardType = "Fisico", Description = "Patron recurrente de ruido excesivo", Frequency = 4, FirstOccurrenceDate = DateTime.UtcNow.AddDays(-30), AnalysisPeriodDays = 30, IsReviewed = false });
                await context.SaveChangesAsync();
            }

            if (!await context.PatternAlerts.AnyAsync())
            {
                context.PatternAlerts.Add(new PatternAlert { Id = "PA_001", PatternId = "RP_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", OccurrenceCount = 4, FirstReportDate = DateTime.UtcNow.AddDays(-30), Status = "Active", GenerationDate = DateTime.UtcNow });
                await context.SaveChangesAsync();
            }

            if (!await context.AreaCriticalityLevels.AnyAsync())
            {
                context.AreaCriticalityLevels.Add(new AreaCriticalityLevel { Id = "ACL_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", CriticalityLevel = "Importante", MapIntensity = "alta", LastUpdated = DateTime.UtcNow });
                await context.SaveChangesAsync();
            }

            if (!await context.DailySummaries.AnyAsync())
            {
                context.DailySummaries.AddRange(
                    new DailySummary { Id = "DS_001", Date = DateTime.UtcNow.Date, SectorId = "SECTOR_01", Sector = "Zona de Forjado", TotalNew = 1, TotalInProgress = 2, TotalResolved = 0 },
                    new DailySummary { Id = "DS_002", Date = DateTime.UtcNow.Date.AddDays(-1), SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", TotalNew = 2, TotalInProgress = 1, TotalResolved = 1 }
                );
                await context.SaveChangesAsync();
            }

            // ── Mitigation ──
            if (!await context.Mitigations.AnyAsync())
            {
                context.Mitigations.AddRange(
                    new Mitigation { Id = "MIT_001", RiskAssessmentId = "RA_001", Code = "MIT-001", Description = "Implementar barreras acusticas en forjado", Responsible = "Juan Perez", AssignedDate = DateTime.UtcNow.AddDays(-3), Status = "InProgress", Observations = "Compra de materiales pendiente" },
                    new Mitigation { Id = "MIT_002", RiskAssessmentId = "RA_002", Code = "MIT-002", Description = "Instalar ducha de emergencia en almacen de quimicos", Responsible = "Maria Garcia", AssignedDate = DateTime.UtcNow.AddDays(-1), Status = "Pending", Observations = "Pendiente de aprobacion de presupuesto" },
                    new Mitigation { Id = "MIT_003", RiskAssessmentId = "RA_003", Code = "MIT-003", Description = "Senalizar zona de posturas forzadas", Responsible = "Lucia Fernandez", AssignedDate = DateTime.UtcNow.AddDays(-1), Status = "Pending", Observations = "Capacitacion programada" }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.CorrectiveActionTickets.AnyAsync())
            {
                context.CorrectiveActionTickets.AddRange(
                    new CorrectiveActionTicket { Id = "TKT_001", TicketNumber = 1001, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "InProgress", Instructions = "Instalar barreras acusticas antes de 48h", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = DateTime.UtcNow.AddDays(-1), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_002", TicketNumber = 1002, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Open", Instructions = "Instalar sistema de contencion de derrames", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = DateTime.UtcNow.AddHours(-12), SlaLimitHours = 24, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_003", TicketNumber = 1003, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Open", Instructions = "Colocar pausas activas y senalizacion", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = DateTime.UtcNow.AddHours(-6), SlaLimitHours = 72, SlaMissed = false }
                );
                await context.SaveChangesAsync();
            }

            // Enrichment: ensure a healthy number of corrective tickets across months/sectors
            // so dashboards, trends and the SST plan have meaningful data.
            if (await context.CorrectiveActionTickets.CountAsync() < 30)
            {
                var currentYear = DateTime.UtcNow.Year;
                var extraTickets = new[]
                {
                    // Existing tickets (keep)
                    new CorrectiveActionTicket { Id = "TKT_004", TicketNumber = 1004, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "Closed", ClosureDate = DateTime.UtcNow.AddMonths(-2).AddDays(3), Instructions = "Reforzar barreras acusticas en forjado", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = DateTime.UtcNow.AddMonths(-2), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_005", TicketNumber = 1005, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Closed", ClosureDate = DateTime.UtcNow.AddMonths(-2).AddDays(2), Instructions = "Ducha de emergencia instalada", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = DateTime.UtcNow.AddMonths(-2).AddDays(-1), SlaLimitHours = 24, SlaMissed = true },
                    new CorrectiveActionTicket { Id = "TKT_006", TicketNumber = 1006, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Closed", ClosureDate = DateTime.UtcNow.AddMonths(-1).AddDays(4), Instructions = "Pausas activas implementadas", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = DateTime.UtcNow.AddMonths(-1), SlaLimitHours = 72, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_007", TicketNumber = 1007, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "InProgress", Instructions = "Reemplazar aislamiento de maquina", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = DateTime.UtcNow.AddDays(-20), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_008", TicketNumber = 1008, ReportId = "RA_002", SectorId = "SECTOR_04", Sector = "Zona de Soldadura", RiskType = "Fisico", CriticalityLevel = "High", Status = "Open", Instructions = "Pantallas de soldadura en zona comun", AssignedTechnicianId = "TEC_003", TechnicianName = "Juan Perez Ramirez", CreatedDate = DateTime.UtcNow.AddDays(-15), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_009", TicketNumber = 1009, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Open", Instructions = "Rediseno de estacion de trabajo", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = DateTime.UtcNow.AddDays(-10), SlaLimitHours = 72, SlaMissed = false },

                    // Additional 21 tickets for comprehensive dashboard data (30+ total)
                    // Month 1 (Enero) - 3 tickets
                    new CorrectiveActionTicket { Id = "TKT_010", TicketNumber = 1010, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "Closed", ClosureDate = new DateTime(currentYear, 1, 15), Instructions = "Mantenimiento preventivo prensa #3", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 1, 5), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_011", TicketNumber = 1011, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Closed", ClosureDate = new DateTime(currentYear, 1, 20), Instructions = "Instalacion de contencion secundaria", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = new DateTime(currentYear, 1, 8), SlaLimitHours = 24, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_012", TicketNumber = 1012, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Closed", ClosureDate = new DateTime(currentYear, 1, 25), Instructions = "Rotacion de puestos y pausas activas", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = new DateTime(currentYear, 1, 12), SlaLimitHours = 72, SlaMissed = false },

                    // Month 2 (Febrero) - 3 tickets
                    new CorrectiveActionTicket { Id = "TKT_013", TicketNumber = 1013, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "Closed", ClosureDate = new DateTime(currentYear, 2, 10), Instructions = "Sustitucion de aislamiento termico", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 2, 1), SlaLimitHours = 48, SlaMissed = true },
                    new CorrectiveActionTicket { Id = "TKT_014", TicketNumber = 1014, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Closed", ClosureDate = new DateTime(currentYear, 2, 18), Instructions = "Kit derrames y señalizacion", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = new DateTime(currentYear, 2, 5), SlaLimitHours = 24, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_015", TicketNumber = 1015, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Closed", ClosureDate = new DateTime(currentYear, 2, 22), Instructions = "Herramientas ergonomicas", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = new DateTime(currentYear, 2, 10), SlaLimitHours = 72, SlaMissed = false },

                    // Month 3 (Marzo) - 3 tickets
                    new CorrectiveActionTicket { Id = "TKT_016", TicketNumber = 1016, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "Closed", ClosureDate = new DateTime(currentYear, 3, 12), Instructions = "Barreras acusticas adicionales", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 3, 2), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_017", TicketNumber = 1017, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Closed", ClosureDate = new DateTime(currentYear, 3, 20), Instructions = "Ventilacion forzada almacen", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = new DateTime(currentYear, 3, 8), SlaLimitHours = 24, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_018", TicketNumber = 1018, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Closed", ClosureDate = new DateTime(currentYear, 3, 28), Instructions = "Mesas de trabajo ajustables", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = new DateTime(currentYear, 3, 15), SlaLimitHours = 72, SlaMissed = false },

                    // Month 4 (Abril) - 3 tickets
                    new CorrectiveActionTicket { Id = "TKT_019", TicketNumber = 1019, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "Closed", ClosureDate = new DateTime(currentYear, 4, 14), Instructions = "Mantenimiento prensa hidraulica", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 4, 3), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_020", TicketNumber = 1020, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Closed", ClosureDate = new DateTime(currentYear, 4, 22), Instructions = "Detector de gases instalado", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = new DateTime(currentYear, 4, 10), SlaLimitHours = 24, SlaMissed = true },
                    new CorrectiveActionTicket { Id = "TKT_021", TicketNumber = 1021, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Closed", ClosureDate = new DateTime(currentYear, 4, 29), Instructions = "Capacitacion manipulacion cargas", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = new DateTime(currentYear, 4, 18), SlaLimitHours = 72, SlaMissed = false },

                    // Month 5 (Mayo) - 3 tickets
                    new CorrectiveActionTicket { Id = "TKT_022", TicketNumber = 1022, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "Closed", ClosureDate = new DateTime(currentYear, 5, 16), Instructions = "Silenciadores escape prensa", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 5, 5), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_023", TicketNumber = 1023, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Closed", ClosureDate = new DateTime(currentYear, 5, 24), Instructions = "Etiquetado GHS completo", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = new DateTime(currentYear, 5, 12), SlaLimitHours = 24, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_024", TicketNumber = 1024, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Closed", ClosureDate = new DateTime(currentYear, 5, 30), Instructions = "Apoyos antifatiga instalados", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = new DateTime(currentYear, 5, 20), SlaLimitHours = 72, SlaMissed = false },

                    // Month 6 (Junio) - 3 tickets
                    new CorrectiveActionTicket { Id = "TKT_025", TicketNumber = 1025, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "InProgress", Instructions = "Revision sistematica vibraciones", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 6, 3), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_026", TicketNumber = 1026, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Open", Instructions = "Sistema supresion incendios", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = new DateTime(currentYear, 6, 10), SlaLimitHours = 24, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_027", TicketNumber = 1027, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Open", Instructions = "Rediseño puesto soldador", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = new DateTime(currentYear, 6, 18), SlaLimitHours = 72, SlaMissed = false },

                    // Month 7 (Julio) - 3 tickets
                    new CorrectiveActionTicket { Id = "TKT_028", TicketNumber = 1028, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "Open", Instructions = "Monitor ruido continuo", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 7, 1), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_029", TicketNumber = 1029, ReportId = "RA_002", SectorId = "SECTOR_02", Sector = "Almacen de Quimicos", RiskType = "Quimico", CriticalityLevel = "Critical", Status = "Open", Instructions = "Inventario quimicos actualizado", AssignedTechnicianId = "TEC_002", TechnicianName = "Maria Garcia Torres", CreatedDate = new DateTime(currentYear, 7, 5), SlaLimitHours = 24, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_030", TicketNumber = 1030, ReportId = "RA_003", SectorId = "SECTOR_03", Sector = "Linea de Ensamblaje", RiskType = "Ergonomico", CriticalityLevel = "Medium", Status = "Open", Instructions = "Evaluacion psicosocial", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = new DateTime(currentYear, 7, 8), SlaLimitHours = 72, SlaMissed = false },

                    // Additional sectors: Zona de Soldadura, Taller Mantenimiento, Zona Pintura
                    new CorrectiveActionTicket { Id = "TKT_031", TicketNumber = 1031, ReportId = "RA_001", SectorId = "SECTOR_04", Sector = "Zona de Soldadura", RiskType = "Fisico", CriticalityLevel = "High", Status = "Closed", ClosureDate = new DateTime(currentYear, 3, 15), Instructions = "Pantallas soldadura autooscurec", AssignedTechnicianId = "TEC_003", TechnicianName = "Juan Perez Ramirez", CreatedDate = new DateTime(currentYear, 3, 5), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_032", TicketNumber = 1032, ReportId = "RA_002", SectorId = "SECTOR_06", Sector = "Taller de Mantenimiento", RiskType = "Mecanico", CriticalityLevel = "Medium", Status = "Closed", ClosureDate = new DateTime(currentYear, 4, 20), Instructions = "Orden y limpieza 5S", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 4, 10), SlaLimitHours = 72, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_033", TicketNumber = 1033, ReportId = "RA_003", SectorId = "SECTOR_07", Sector = "Zona de Pintura", RiskType = "Quimico", CriticalityLevel = "High", Status = "Closed", ClosureDate = new DateTime(currentYear, 5, 10), Instructions = "Cabina ventilacion certificada", AssignedTechnicianId = "TEC_004", TechnicianName = "Lucia Fernandez Diaz", CreatedDate = new DateTime(currentYear, 5, 1), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_034", TicketNumber = 1034, ReportId = "RA_001", SectorId = "SECTOR_04", Sector = "Zona de Soldadura", RiskType = "Fisico", CriticalityLevel = "High", Status = "InProgress", Instructions = "Extraccion humos soldadura", AssignedTechnicianId = "TEC_003", TechnicianName = "Juan Perez Ramirez", CreatedDate = new DateTime(currentYear, 6, 1), SlaLimitHours = 48, SlaMissed = false },
                    new CorrectiveActionTicket { Id = "TKT_035", TicketNumber = 1035, ReportId = "RA_002", SectorId = "SECTOR_06", Sector = "Taller de Mantenimiento", RiskType = "Mecanico", CriticalityLevel = "Medium", Status = "Open", Instructions = "Calibrado herramientas torque", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = new DateTime(currentYear, 7, 2), SlaLimitHours = 72, SlaMissed = false }
                };
                foreach (var t in extraTickets)
                {
                    if (await context.CorrectiveActionTickets.FindAsync(t.Id) == null)
                        context.CorrectiveActionTickets.Add(t);
                }
                await context.SaveChangesAsync();
            }

            if (!await context.MeasureVerifications.AnyAsync())
            {
                context.MeasureVerifications.AddRange(
                    new MeasureVerification { Id = "VER_001", TicketId = "TKT_001", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Medidas implementadas correctamente", VerificationDate = DateTime.UtcNow },
                    new MeasureVerification { Id = "VER_002", TicketId = "TKT_004", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Barreras acústicas reforzadas y validadas", VerificationDate = DateTime.UtcNow.AddMonths(-2).AddDays(5) },
                    new MeasureVerification { Id = "VER_003", TicketId = "TKT_005", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Ducha de emergencia instalada y probada", VerificationDate = DateTime.UtcNow.AddMonths(-2).AddDays(4) },
                    new MeasureVerification { Id = "VER_004", TicketId = "TKT_006", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Pausas activas implementadas en línea", VerificationDate = DateTime.UtcNow.AddMonths(-1).AddDays(6) },
                    new MeasureVerification { Id = "VER_005", TicketId = "TKT_010", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Mantenimiento preventivo completado", VerificationDate = new DateTime(currentYear, 1, 17) },
                    new MeasureVerification { Id = "VER_006", TicketId = "TKT_011", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Contención secundaria operativa", VerificationDate = new DateTime(currentYear, 1, 22) },
                    new MeasureVerification { Id = "VER_007", TicketId = "TKT_012", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Rotación de puestos establecida", VerificationDate = new DateTime(currentYear, 1, 27) },
                    new MeasureVerification { Id = "VER_008", TicketId = "TKT_013", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Aislamiento térmico sustituido", VerificationDate = new DateTime(currentYear, 2, 12) },
                    new MeasureVerification { Id = "VER_009", TicketId = "TKT_014", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Kit derrames y señalización instalados", VerificationDate = new DateTime(currentYear, 2, 20) },
                    new MeasureVerification { Id = "VER_010", TicketId = "TKT_015", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Herramientas ergonómicas entregadas", VerificationDate = new DateTime(currentYear, 2, 24) },
                    new MeasureVerification { Id = "VER_011", TicketId = "TKT_016", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Barreras acústicas adicionales instaladas", VerificationDate = new DateTime(currentYear, 3, 14) },
                    new MeasureVerification { Id = "VER_012", TicketId = "TKT_017", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Ventilación forzada operativa", VerificationDate = new DateTime(currentYear, 3, 22) },
                    new MeasureVerification { Id = "VER_013", TicketId = "TKT_018", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Mesas ajustables instaladas", VerificationDate = new DateTime(currentYear, 3, 30) },
                    new MeasureVerification { Id = "VER_014", TicketId = "TKT_019", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Mantenimiento prensa completado", VerificationDate = new DateTime(currentYear, 4, 16) },
                    new MeasureVerification { Id = "VER_015", TicketId = "TKT_020", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Detector de gases instalado y calibrado", VerificationDate = new DateTime(currentYear, 4, 24) },
                    new MeasureVerification { Id = "VER_016", TicketId = "TKT_021", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Capacitación realizada a todo el personal", VerificationDate = new DateTime(currentYear, 5, 1) },
                    new MeasureVerification { Id = "VER_017", TicketId = "TKT_022", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Silenciadores instalados en escapes", VerificationDate = new DateTime(currentYear, 5, 18) },
                    new MeasureVerification { Id = "VER_018", TicketId = "TKT_023", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Etiquetado GHS completado al 100%", VerificationDate = new DateTime(currentYear, 5, 26) },
                    new MeasureVerification { Id = "VER_019", TicketId = "TKT_024", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Apoyos antifatiga en todas las estaciones", VerificationDate = new DateTime(currentYear, 6, 1) },
                    new MeasureVerification { Id = "VER_020", TicketId = "TKT_031", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Pantallas autooscurecidas validadas", VerificationDate = new DateTime(currentYear, 3, 17) },
                    new MeasureVerification { Id = "VER_021", TicketId = "TKT_032", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "5S implementado en taller", VerificationDate = new DateTime(currentYear, 4, 22) },
                    new MeasureVerification { Id = "VER_022", TicketId = "TKT_033", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Cabina certificación vigente", VerificationDate = new DateTime(currentYear, 5, 12) }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.TicketHistories.AnyAsync())
            {
                context.TicketHistories.Add(new TicketHistory { Id = "HIST_001", TicketId = "TKT_001", Event = "Ticket creado", UserName = "Supervisor RiskGuard", Details = "Ticket creado para instalacion de barreras acusticas", Date = DateTime.UtcNow.AddDays(-1) });
                await context.SaveChangesAsync();
            }

            if (!await context.SlaAlerts.AnyAsync())
            {
                context.SlaAlerts.AddRange(
                    new SlaAlert { Id = "SLA_001", TicketId = "TKT_001", ElapsedHours = 24, SlaLimitHours = 48, AlertDate = DateTime.UtcNow, NotifiedName = "Supervisor RiskGuard" },
                    new SlaAlert { Id = "SLA_002", TicketId = "TKT_005", ElapsedHours = 26, SlaLimitHours = 24, AlertDate = DateTime.UtcNow.AddMonths(-2).AddDays(1), NotifiedName = "Supervisor RiskGuard" },
                    new SlaAlert { Id = "SLA_003", TicketId = "TKT_007", ElapsedHours = 36, SlaLimitHours = 48, AlertDate = DateTime.UtcNow.AddDays(-18), NotifiedName = "Supervisor RiskGuard" },
                    new SlaAlert { Id = "SLA_004", TicketId = "TKT_013", ElapsedHours = 50, SlaLimitHours = 48, AlertDate = new DateTime(currentYear, 2, 3), NotifiedName = "Supervisor RiskGuard" },
                    new SlaAlert { Id = "SLA_005", TicketId = "TKT_020", ElapsedHours = 28, SlaLimitHours = 24, AlertDate = new DateTime(currentYear, 4, 12), NotifiedName = "Supervisor RiskGuard" },
                    new SlaAlert { Id = "SLA_006", TicketId = "TKT_025", ElapsedHours = 30, SlaLimitHours = 48, AlertDate = new DateTime(currentYear, 6, 5), NotifiedName = "Supervisor RiskGuard" },
                    new SlaAlert { Id = "SLA_007", TicketId = "TKT_026", ElapsedHours = 20, SlaLimitHours = 24, AlertDate = new DateTime(currentYear, 6, 12), NotifiedName = "Supervisor RiskGuard" },
                    new SlaAlert { Id = "SLA_008", TicketId = "TKT_034", ElapsedHours = 40, SlaLimitHours = 48, AlertDate = new DateTime(currentYear, 6, 3), NotifiedName = "Supervisor RiskGuard" }
                );
                await context.SaveChangesAsync();
            }

            // ── CriticalNotifications (was never seeded) ──
            if (!await context.CriticalNotifications.AnyAsync())
            {
                context.CriticalNotifications.AddRange(
                    new CriticalNotification { Id = "CN_001", TicketId = "TKT_002", SupervisorId = supervisorUser!.Id, SupervisorName = "Supervisor RiskGuard", Message = "Ticket critico #1002 requiere decision administrativa.", Sent = false, SentDate = DateTime.UtcNow.AddHours(-12) },
                    new CriticalNotification { Id = "CN_002", TicketId = "TKT_001", SupervisorId = supervisorUser!.Id, SupervisorName = "Supervisor RiskGuard", Message = "Ticket #1001 con SLA por vencer.", Sent = true, SentDate = DateTime.UtcNow.AddHours(-20) }
                );
                await context.SaveChangesAsync();
            }

            // ── Dangers / Inspections ──
            if (!await context.Dangers.AnyAsync())
            {
                context.Dangers.AddRange(
                    new Danger { Id = 1, Name = "Piso resbaloso", Category = "Condicion insegura", Description = "Superficie con derrame de lubricante" },
                    new Danger { Id = 2, Name = "Cable expuesto", Category = "Condicion insegura", Description = "Cable electrico sin proteccion" },
                    new Danger { Id = 3, Name = "Extintor vencido", Category = "Equipo de emergencia", Description = "Extintor con fecha de vencimiento pasada" }
                );
                await context.SaveChangesAsync();
            }

            if (!await context.Inspections.AnyAsync())
            {
                context.Inspections.AddRange(
                    new Inspection { Id = 1, Ticket = "INSP-001", IncidentType = "Condicion insegura", AreaId = 1, HeadquartersId = 1, AssetId = 1, UrgencyLevel = "High", Description = "Ruido excesivo en prensa hidraulica", Status = "Completed", OperatorId = operarioUser!.Id, ReportDate = DateTime.UtcNow.AddDays(-7), UpdateDate = DateTime.UtcNow.AddDays(-5), CorrectiveAction = "Instalar barreras acusticas" },
                    new Inspection { Id = 2, Ticket = "INSP-002", IncidentType = "Practica insegura", AreaId = 2, HeadquartersId = 1, UrgencyLevel = "Critical", Description = "Derrame de solvente en almacen", Status = "InProgress", OperatorId = operarioUser!.Id, ReportDate = DateTime.UtcNow.AddDays(-3) },
                    new Inspection { Id = 3, Ticket = "INSP-003", IncidentType = "Condicion insegura", AreaId = 4, HeadquartersId = 2, AssetId = 3, UrgencyLevel = "Medium", Description = "Cableado expuesto en robot soldador", Status = "Pending", OperatorId = operarioUser!.Id, ReportDate = DateTime.UtcNow.AddDays(-1) },
                    new Inspection { Id = 4, Ticket = "INSP-004", IncidentType = "Equipo de proteccion", AreaId = 3, HeadquartersId = 1, UrgencyLevel = "Low", Description = "Falta de EPP en linea de ensamblaje", Status = "Pending", OperatorId = operarioUser!.Id, ReportDate = DateTime.UtcNow },
                    new Inspection { Id = 5, Ticket = "INSP-005", IncidentType = "Condicion insegura", AreaId = 6, HeadquartersId = 3, AssetId = 5, UrgencyLevel = "High", Description = "Fuga de aire en compresor", Status = "Pending", OperatorId = operarioUser!.Id, ReportDate = DateTime.UtcNow },
                    new Inspection { Id = 6, Ticket = "INSP-006", IncidentType = "Condicion insegura", AreaId = 7, HeadquartersId = 4, AssetId = 9, UrgencyLevel = "Medium", Description = "Acumulacion de residuos en cabina de pintura", Status = "Pending", OperatorId = operarioUser2!.Id, ReportDate = DateTime.UtcNow.AddHours(-8) },
                    new Inspection { Id = 7, Ticket = "INSP-007", IncidentType = "Equipo de emergencia", AreaId = 8, HeadquartersId = 4, AssetId = 10, UrgencyLevel = "Low", Description = "Extintor vencido en patio de almacenamiento", Status = "Completed", OperatorId = operarioUser2!.Id, ReportDate = DateTime.UtcNow.AddDays(-2), UpdateDate = DateTime.UtcNow.AddDays(-1), CorrectiveAction = "Reemplazar extintor" }
                );
                await context.SaveChangesAsync();
            }

            // ── PhotoEvidence (was never seeded) ──
            if (!await context.PhotoEvidences.AnyAsync())
            {
                context.PhotoEvidences.Add(new PhotoEvidence { InspectionId = 1, FileUrl = "/evidence/inspeccion_1_evidencia.jpg", UploadDate = DateTime.UtcNow.AddDays(-7) });
                await context.SaveChangesAsync();
            }

            // ── Monitoring: HeatMapZones ──
            if (!await context.HeatMapZones.AnyAsync())
            {
                context.HeatMapZones.AddRange(
                    new HeatMapZone { Id = 1, Name = "Zona de Forjado", SectorId = 1, HeatIndex = 85.5m, RiskLevel = "High", LastUpdate = DateTime.UtcNow },
                    new HeatMapZone { Id = 2, Name = "Almacen de Quimicos", SectorId = 2, HeatIndex = 92.0m, RiskLevel = "Critical", LastUpdate = DateTime.UtcNow },
                    new HeatMapZone { Id = 3, Name = "Linea de Ensamblaje", SectorId = 3, HeatIndex = 45.0m, RiskLevel = "Low", LastUpdate = DateTime.UtcNow },
                    new HeatMapZone { Id = 4, Name = "Zona de Soldadura", SectorId = 4, HeatIndex = 70.5m, RiskLevel = "Medium", LastUpdate = DateTime.UtcNow }
                );
                await context.SaveChangesAsync();
            }

            // ── Monitoring: Technicians ──
            if (!await context.DashboardTechnicians.AnyAsync())
            {
                var monTech1 = new MonitorTechnician { Id = Guid.NewGuid().ToString("N"), Name = "Carlos Mendoza Lopez", Specialty = "Electricidad", Status = "Active" };
                var monTech2 = new MonitorTechnician { Id = Guid.NewGuid().ToString("N"), Name = "Maria Garcia Torres", Specialty = "Mecanica", Status = "Active" };
                context.DashboardTechnicians.AddRange(monTech1, monTech2);
                await context.SaveChangesAsync();
            }

            var monTech1Id = (await context.DashboardTechnicians.OrderBy(t => t.Id).FirstOrDefaultAsync())?.Id;
            var monTech2Id = (await context.DashboardTechnicians.OrderByDescending(t => t.Id).FirstOrDefaultAsync())?.Id;

            // ── Monitoring: Tickets ──
            if (!await context.DashboardTickets.AnyAsync())
            {
                context.DashboardTickets.AddRange(
                    new MonitorTicket { Id = 1, SectorId = 1, Title = "Ruido excesivo en forjado", Status = "InProgress", Priority = "High", AssignedTechnicianId = monTech1Id, CreatedAt = DateTime.UtcNow.AddDays(-2) },
                    new MonitorTicket { Id = 2, SectorId = 2, Title = "Derrame quimico pendiente", Status = "Pending", Priority = "Critical", AssignedTechnicianId = monTech2Id, CreatedAt = DateTime.UtcNow.AddDays(-1) },
                    new MonitorTicket { Id = 3, SectorId = 4, Title = "Cableado expuesto en robot", Status = "Scheduled", Priority = "Medium", AssignedTechnicianId = monTech1Id, CreatedAt = DateTime.UtcNow }
                );
                await context.SaveChangesAsync();
            }

            // ── Monitoring: Assets ──
            if (!await context.DashboardAssets.AnyAsync())
            {
                context.DashboardAssets.AddRange(
                    new MonitorAsset { Id = Guid.NewGuid().ToString("N"), Name = "Prensa Hidraulica #3", Code = "PH-003", SectorId = 1, Status = "Active" },
                    new MonitorAsset { Id = Guid.NewGuid().ToString("N"), Name = "Robot Soldador RS-200", Code = "RS-200", SectorId = 4, Status = "Active" },
                    new MonitorAsset { Id = Guid.NewGuid().ToString("N"), Name = "Compresor CA-100", Code = "CA-100", SectorId = 3, Status = "Active" }
                );
                await context.SaveChangesAsync();
            }

            // ── Monitoring: PreventiveMaintenances ──
            if (!await context.PreventiveMaintenances.AnyAsync())
            {
                context.PreventiveMaintenances.AddRange(
                    new PreventiveMaintenance { Id = Guid.NewGuid().ToString("N"), AssetId = "PH-003", Description = "Mantenimiento trimestral prensa hidraulica", Status = "Scheduled", ScheduledDate = DateTime.UtcNow.AddDays(15) },
                    new PreventiveMaintenance { Id = Guid.NewGuid().ToString("N"), AssetId = "RS-200", Description = "Revision de brazos roboticos", Status = "InProgress", ScheduledDate = DateTime.UtcNow }
                );
                await context.SaveChangesAsync();
            }

            // ── Monitoring: ArchivedReports ──
            if (!await context.ArchivedReports.AnyAsync())
            {
                context.ArchivedReports.AddRange(
                    new ArchivedReport { Id = Guid.NewGuid().ToString("N"), Title = "Reporte de Seguridad Mayo 2026", Url = "/reports/seguridad-mayo-2026.pdf", HashIntegrity = "a1b2c3d4e5f6", ArchiveDate = DateTime.UtcNow.AddDays(-30) },
                    new ArchivedReport { Id = Guid.NewGuid().ToString("N"), Title = "Reporte de Cumplimiento Junio 2026", Url = "/reports/cumplimiento-junio-2026.pdf", HashIntegrity = "f6e5d4c3b2a1", ArchiveDate = DateTime.UtcNow.AddDays(-2) }
                );
                await context.SaveChangesAsync();
            }

            logger.LogInformation("RiskGuard database seeding completed successfully.");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "RiskGuard database seeding failed. Check MySQL availability and connection string.");
            throw;
        }
    }
}
