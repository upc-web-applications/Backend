using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Inspections.Domain.Model.Aggregates;

namespace RiskGuard.Platform.Inspections.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyInspectionsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Inspeccion>().HasKey(inspection => inspection.Id);
        builder.Entity<Inspeccion>().Property(inspection => inspection.Id).ValueGeneratedOnAdd();
        builder.Entity<Inspeccion>().HasIndex(inspection => inspection.Ticket).IsUnique();
        builder.Entity<Inspeccion>().Property(inspection => inspection.Descripcion).HasMaxLength(300);

        builder.Entity<Peligro>().HasKey(peligro => peligro.Id);
        builder.Entity<Peligro>().Property(peligro => peligro.Id).ValueGeneratedOnAdd();

        builder.Entity<EvidenciaFotografica>().HasKey(evidence => evidence.Id);
        builder.Entity<EvidenciaFotografica>().Property(evidence => evidence.Id).ValueGeneratedOnAdd();
    }
}
