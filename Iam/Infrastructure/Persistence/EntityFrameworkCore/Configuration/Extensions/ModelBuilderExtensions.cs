using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Iam.Domain.Model.Aggregates;

namespace RiskGuard.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Role>().HasKey(role => role.Id);
        builder.Entity<Role>().Property(role => role.Code).IsRequired().HasMaxLength(50);
        builder.Entity<Role>().Property(role => role.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Role>().HasIndex(role => role.Name).IsUnique();
        builder.Entity<Role>().HasIndex(role => role.Code).IsUnique();

        builder.Entity<User>().HasKey(user => user.Id);
        builder.Entity<User>().Property(user => user.Email).IsRequired().HasMaxLength(120);
        builder.Entity<User>().Property(user => user.Name).IsRequired().HasMaxLength(150);
        builder.Entity<User>().Property(user => user.PasswordHash).IsRequired();
        builder.Entity<User>().HasIndex(user => user.Email).IsUnique();

        builder.Entity<Session>().HasKey(session => session.Id);
        builder.Entity<Session>().Property(session => session.TokenSignature).IsRequired();

        builder.Entity<AccessLog>().HasKey(log => log.Id);
    }
}
