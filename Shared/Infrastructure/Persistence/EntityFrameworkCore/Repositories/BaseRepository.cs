using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

namespace RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext Context = context;

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }
}
