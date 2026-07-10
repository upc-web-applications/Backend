using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.OrganizationAssets.Domain.Model.Aggregates;

namespace Acme.Center.Platform.OrganizationAssets.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyOrganizationAssetsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Headquarters>().HasKey(hq => hq.Id);
        builder.Entity<Headquarters>().Property(hq => hq.Id).ValueGeneratedOnAdd();
        builder.Entity<Headquarters>().HasIndex(hq => hq.Name).IsUnique();

        builder.Entity<Area>().HasKey(area => area.Id);
        builder.Entity<Area>().Property(area => area.Id).ValueGeneratedOnAdd();
        builder.Entity<Area>().HasIndex(area => area.Code).IsUnique();

        builder.Entity<Asset>().HasKey(asset => asset.Id);
        builder.Entity<Asset>().Property(asset => asset.Id).ValueGeneratedOnAdd();
        builder.Entity<Asset>().HasIndex(asset => asset.Code).IsUnique();
        builder.Entity<Asset>().Property(asset => asset.Type).HasMaxLength(100);
    }
}
