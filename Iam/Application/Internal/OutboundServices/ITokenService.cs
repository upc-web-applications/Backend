using Acme.Center.Platform.Iam.Domain.Model.Aggregates;

namespace Acme.Center.Platform.Iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<string?> ValidateToken(string token);
}
