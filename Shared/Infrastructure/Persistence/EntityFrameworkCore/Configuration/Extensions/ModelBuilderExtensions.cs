using Microsoft.EntityFrameworkCore;

namespace RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName)) entity.SetTableName(tableName.ToPlural().ToSnakeCase());

            foreach (var property in entity.GetProperties())
                property.SetColumnName(property.GetColumnName().ToSnakeCase());

            foreach (var key in entity.GetKeys())
                if (!string.IsNullOrEmpty(key.GetName()))
                    key.SetName(key.GetName()!.ToSnakeCase());

            foreach (var foreignKey in entity.GetForeignKeys())
                if (!string.IsNullOrEmpty(foreignKey.GetConstraintName()))
                    foreignKey.SetConstraintName(foreignKey.GetConstraintName()!.ToSnakeCase());

            foreach (var index in entity.GetIndexes())
                if (!string.IsNullOrEmpty(index.GetDatabaseName()))
                    index.SetDatabaseName(index.GetDatabaseName()!.ToSnakeCase());
        }
    }
}
