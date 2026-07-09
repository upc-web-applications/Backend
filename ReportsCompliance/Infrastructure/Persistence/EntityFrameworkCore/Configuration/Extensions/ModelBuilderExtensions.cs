using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.ReportsCompliance.Domain.Model.Aggregates;

namespace Acme.Center.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyReportsComplianceConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HistoricalIncidentRecord>().HasKey(record => record.Id);
        builder.Entity<AnnualOhsPlan>().HasKey(plan => plan.Id);
        builder.Entity<PredictiveIndicator>().HasKey(indicator => indicator.Id);
        builder.Entity<CriticalAlert>().HasKey(alert => alert.Id);
        builder.Entity<GeneratedReport>().HasKey(report => report.Id);
        builder.Entity<KpiDashboard>().HasKey(kpi => kpi.Id);
        builder.Entity<HistoricalTrend>().HasKey(trend => trend.Id);
    }
}
