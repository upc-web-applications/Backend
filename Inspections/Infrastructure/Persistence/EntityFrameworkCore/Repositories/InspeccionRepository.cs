using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Inspections.Domain.Model.Aggregates;
using Acme.Center.Platform.Inspections.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Acme.Center.Platform.Inspections.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class InspectionRepository(AppDbContext context) : BaseRepository<Inspection>(context), IInspectionRepository
{
    public async Task<IEnumerable<Inspection>> ListByOperatorAsync(string operatorId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Inspections
            .Where(inspection => inspection.OperatorId == operatorId)
            .OrderByDescending(inspection => inspection.ReportDate)
            .ToListAsync(cancellationToken);
    }
}
