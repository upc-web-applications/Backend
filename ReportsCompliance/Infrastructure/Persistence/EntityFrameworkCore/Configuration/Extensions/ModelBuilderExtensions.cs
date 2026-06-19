using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.ReportsCompliance.Domain.Model.Aggregates;

namespace RiskGuard.Platform.ReportsCompliance.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyReportsComplianceConfiguration(this ModelBuilder builder)
    {
        builder.Entity<MonthlyReport>().HasKey(report => report.Id);
        builder.Entity<CumulativeStIndicator>().HasKey(indicator => indicator.Id);
    }
}
