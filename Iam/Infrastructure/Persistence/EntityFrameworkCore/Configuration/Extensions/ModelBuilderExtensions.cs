using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Iam.Domain.Model.Aggregates;

namespace Acme.Center.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired();
        builder.Entity<User>().Property(u => u.Username).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        builder.Entity<Role>().HasKey(r => r.Id);
        builder.Entity<Role>().Property(r => r.Id).IsRequired();
        builder.Entity<Role>().Property(r => r.Code).IsRequired().HasMaxLength(50);
        builder.Entity<Role>().HasIndex(r => r.Code).IsUnique();

        builder.Entity<Session>().HasKey(s => s.Id);
        builder.Entity<Session>().Property(s => s.Id).IsRequired();

        builder.Entity<AccessLog>().HasKey(al => al.Id);
        builder.Entity<AccessLog>().Property(al => al.Id).IsRequired();
    }
}
