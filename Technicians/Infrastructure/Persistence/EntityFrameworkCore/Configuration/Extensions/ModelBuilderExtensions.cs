using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;

namespace Acme.Center.Platform.Technicians.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyTechnicianConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Technician>().HasKey(t => t.Id);
        builder.Entity<Technician>().Property(t => t.Id).IsRequired();
        builder.Entity<Technician>().Property(t => t.DocumentNumber).IsRequired().HasMaxLength(20);
        builder.Entity<Technician>().Property(t => t.FullName).IsRequired().HasMaxLength(200);
        builder.Entity<Technician>().Property(t => t.Specialty).HasMaxLength(100);
        builder.Entity<Technician>().Property(t => t.Phone).HasMaxLength(20);
        builder.Entity<Technician>().Property(t => t.Email).HasMaxLength(100);
        builder.Entity<Technician>().Property(t => t.Status).IsRequired().HasMaxLength(50);
    }
}
