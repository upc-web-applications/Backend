using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;

namespace Acme.Center.Platform.Hazards.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyHazardConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Hazard>().HasKey(h => h.Id);
        builder.Entity<Hazard>().Property(h => h.Id).IsRequired();
        builder.Entity<Hazard>().Property(h => h.Code).IsRequired().HasMaxLength(50);
        builder.Entity<Hazard>().Property(h => h.Name).IsRequired().HasMaxLength(200);
        builder.Entity<Hazard>().Property(h => h.Category).IsRequired().HasMaxLength(50);
        builder.Entity<Hazard>().Property(h => h.BaseRiskLevel).IsRequired().HasMaxLength(50);
        builder.Entity<Hazard>().Property(h => h.Status).IsRequired().HasMaxLength(50);
    }
}
