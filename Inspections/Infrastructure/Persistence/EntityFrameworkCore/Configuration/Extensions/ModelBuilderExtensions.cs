using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Inspections.Domain.Model.Aggregates;

namespace Acme.Center.Platform.Inspections.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyInspectionsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Inspection>().HasKey(inspection => inspection.Id);
        builder.Entity<Inspection>().Property(inspection => inspection.Id).ValueGeneratedOnAdd();
        builder.Entity<Inspection>().HasIndex(inspection => inspection.Ticket).IsUnique();
        builder.Entity<Inspection>().Property(inspection => inspection.Description).HasMaxLength(300);

        builder.Entity<Danger>().HasKey(danger => danger.Id);
        builder.Entity<Danger>().Property(danger => danger.Id).ValueGeneratedOnAdd();

        builder.Entity<PhotoEvidence>().HasKey(evidence => evidence.Id);
        builder.Entity<PhotoEvidence>().Property(evidence => evidence.Id).ValueGeneratedOnAdd();
    }
}
