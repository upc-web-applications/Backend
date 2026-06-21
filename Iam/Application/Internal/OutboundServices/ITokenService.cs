using RiskGuard.Platform.Iam.Domain.Model.Aggregates;

namespace RiskGuard.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
}
