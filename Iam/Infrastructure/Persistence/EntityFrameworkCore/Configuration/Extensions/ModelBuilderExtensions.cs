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
    }
}
