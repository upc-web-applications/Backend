using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
