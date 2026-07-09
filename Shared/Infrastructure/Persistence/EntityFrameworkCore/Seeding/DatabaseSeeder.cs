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
            if (await context.Users.AnyAsync()) return;

            // ── IAM: Demo users ──
            context.Users.AddRange(
                new User("admin", BCryptNet.HashPassword("Risk123")) { Email = "admin@riskguard.tech" },
                new User("supervisor", BCryptNet.HashPassword("Risk123")) { Email = "supervisor@riskguard.tech" },
                new User("operario", BCryptNet.HashPassword("Risk123")) { Email = "operario@riskguard.tech" }
            );

            // ── ReportsCompliance ──
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

            // ── Hazard ──
            context.Hazards.AddRange(
                new Hazard { Id = "HAZ_001", Code = "FIS-001", Name = "Exposicion a ruido", Description = "Ruido continuo >85dB en zona de produccion", Category = "Fisico", BaseRiskLevel = "Medium", Status = "Active" },
                new Hazard { Id = "HAZ_002", Code = "QUI-001", Name = "Derrame de solventes", Description = "Manipulacion de solventes organicos", Category = "Quimico", BaseRiskLevel = "High", Status = "Active" },
                new Hazard { Id = "HAZ_003", Code = "ERG-001", Name = "Posturas forzadas", Description = "Levantamiento manual de cargas pesadas", Category = "Ergonomico", BaseRiskLevel = "Medium", Status = "Active" }
            );

            // ── Technician ──
            context.Technicians.AddRange(
                new Technician { Id = "TEC_001", DocumentNumber = "DNI12345678", FullName = "Carlos Mendoza Lopez", Specialty = "Electricidad", Phone = "999111222", Email = "carlos.m@riskguard.tech", Status = "Active" },
                new Technician { Id = "TEC_002", DocumentNumber = "DNI87654321", FullName = "Maria Garcia Torres", Specialty = "Mecanica", Phone = "999333444", Email = "maria.g@riskguard.tech", Status = "Active" }
            );

            // ── RiskAssessment ──
            context.RiskAssessments.AddRange(
                new RiskAssessment { Id = "RA_001", Code = "IPERC-001", Sector = "Zona de Forjado", HazardType = "Fisico", Description = "Evaluacion de ruido en forjado", Probability = 3, Severity = 3, RiskLevel = "High", ControlMeasures = "Usar EPP auditivo", Status = "Completed", EvaluationDate = DateTime.UtcNow.AddDays(-5) },
                new RiskAssessment { Id = "RA_002", Code = "IPERC-002", Sector = "Almacen de Quimicos", HazardType = "Quimico", Description = "Evaluacion de derrame de solventes", Probability = 2, Severity = 4, RiskLevel = "High", ControlMeasures = "Duchas de emergencia", Status = "Pending", EvaluationDate = DateTime.UtcNow.AddDays(-2) }
            );

            context.RiskPatterns.Add(new RiskPattern { Id = "RP_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", IncidentType = "Condicion insegura", HazardType = "Fisico", Description = "Patron recurrente de ruido excesivo", Frequency = 4, FirstOccurrenceDate = DateTime.UtcNow.AddDays(-30), AnalysisPeriodDays = 30, IsReviewed = false });

            context.PatternAlerts.Add(new PatternAlert { Id = "PA_001", PatternId = "RP_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", OccurrenceCount = 4, FirstReportDate = DateTime.UtcNow.AddDays(-30), Status = "Active", GenerationDate = DateTime.UtcNow });

            context.AreaCriticalityLevels.Add(new AreaCriticalityLevel { Id = "ACL_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", CriticalityLevel = "Importante", MapIntensity = "alta", LastUpdated = DateTime.UtcNow });

            context.DailySummaries.Add(new DailySummary { Id = "DS_001", Date = DateTime.UtcNow.Date, SectorId = "SECTOR_01", Sector = "Zona de Forjado", TotalNew = 1, TotalInProgress = 2, TotalResolved = 0 });

            // ── Mitigation ──
            context.Mitigations.Add(new Mitigation { Id = "MIT_001", RiskAssessmentId = "RA_001", Code = "MIT-001", Description = "Implementar barreras acusticas en forjado", Responsible = "Juan Perez", AssignedDate = DateTime.UtcNow.AddDays(-3), Status = "InProgress", Observations = "Compra de materiales pendiente" });

            context.CorrectiveActionTickets.Add(new CorrectiveActionTicket { Id = "TKT_001", TicketNumber = 1001, ReportId = "RA_001", SectorId = "SECTOR_01", Sector = "Zona de Forjado", RiskType = "Fisico", CriticalityLevel = "High", Status = "InProgress", Instructions = "Instalar barreras acusticas antes de 48h", AssignedTechnicianId = "TEC_001", TechnicianName = "Carlos Mendoza Lopez", CreatedDate = DateTime.UtcNow.AddDays(-1), SlaLimitHours = 48, SlaMissed = false });

            context.MeasureVerifications.Add(new MeasureVerification { Id = "VER_001", TicketId = "TKT_001", SupervisorName = "Supervisor RiskGuard", Verdict = "Eficaz", JustificationComment = "Medidas implementadas correctamente", VerificationDate = DateTime.UtcNow });

            context.TicketHistories.Add(new TicketHistory { Id = "HIST_001", TicketId = "TKT_001", Event = "Ticket creado", UserName = "Supervisor RiskGuard", Details = "Ticket creado para instalacion de barreras acusticas", Date = DateTime.UtcNow.AddDays(-1) });

            context.SlaAlerts.Add(new SlaAlert { Id = "SLA_001", TicketId = "TKT_001", ElapsedHours = 24, SlaLimitHours = 48, AlertDate = DateTime.UtcNow, NotifiedName = "Supervisor RiskGuard" });

            await context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "RiskGuard database seeding was skipped. Check MySQL availability and connection string.");
        }
    }
}
