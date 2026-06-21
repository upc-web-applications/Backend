using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Inspections.Domain.Model.Aggregates;
using RiskGuard.Platform.Inspections.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace RiskGuard.Platform.Inspections.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class InspeccionRepository(AppDbContext context) : BaseRepository<Inspeccion>(context), IInspeccionRepository
{
    public async Task<IEnumerable<Inspeccion>> ListByOperatorAsync(string operatorId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Inspecciones
            .Where(inspection => inspection.OperarioId == operatorId)
            .OrderByDescending(inspection => inspection.FechaReporte)
            .ToListAsync(cancellationToken);
    }
}
