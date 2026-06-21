using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.OrganizationAssets.Domain.Model.Aggregates;

namespace RiskGuard.Platform.OrganizationAssets.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyOrganizationAssetsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Sede>().HasKey(sede => sede.Id);
        builder.Entity<Sede>().Property(sede => sede.Id).ValueGeneratedOnAdd();
        builder.Entity<Sede>().HasIndex(sede => sede.Nombre).IsUnique();

        builder.Entity<Area>().HasKey(area => area.Id);
        builder.Entity<Area>().Property(area => area.Id).ValueGeneratedOnAdd();
        builder.Entity<Area>().HasIndex(area => area.Codigo).IsUnique();

        builder.Entity<Activo>().HasKey(activo => activo.Id);
        builder.Entity<Activo>().Property(activo => activo.Id).ValueGeneratedOnAdd();
        builder.Entity<Activo>().HasIndex(activo => activo.Codigo).IsUnique();
    }
}
