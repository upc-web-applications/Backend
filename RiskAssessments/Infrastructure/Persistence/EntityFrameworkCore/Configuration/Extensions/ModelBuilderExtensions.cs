using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.RiskAssessments.Domain.Model.Aggregates;

namespace Acme.Center.Platform.RiskAssessments.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyRiskAssessmentConfiguration(this ModelBuilder builder)
    {
        builder.Entity<RiskAssessment>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.Code).HasMaxLength(50); e.Property(x => x.Sector).HasMaxLength(200); e.Property(x => x.HazardType).HasMaxLength(100); e.Property(x => x.RiskLevel).HasMaxLength(50); e.Property(x => x.Status).HasMaxLength(50); });
        builder.Entity<RiskPattern>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.Sector).HasMaxLength(200); e.Property(x => x.IncidentType).HasMaxLength(100); e.Property(x => x.HazardType).HasMaxLength(100); });
        builder.Entity<PatternAlert>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.Sector).HasMaxLength(200); e.Property(x => x.RiskType).HasMaxLength(100); e.Property(x => x.Status).HasMaxLength(50); });
        builder.Entity<AreaCriticalityLevel>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.Sector).HasMaxLength(200); e.Property(x => x.CriticalityLevel).HasMaxLength(50); e.Property(x => x.MapIntensity).HasMaxLength(50); });
        builder.Entity<DailySummary>(e => { e.HasKey(x => x.Id); e.Property(x => x.Id).IsRequired(); e.Property(x => x.Sector).HasMaxLength(200); });
    }
}
