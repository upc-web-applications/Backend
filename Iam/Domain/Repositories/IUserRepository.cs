using RiskGuard.Platform.Iam.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;

namespace RiskGuard.Platform.Iam.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
}
