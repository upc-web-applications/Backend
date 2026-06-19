using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;
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
            if (await context.MonthlyReports.AnyAsync()) return;

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
