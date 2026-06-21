using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RiskGuard.Platform.Iam.Application.Internal.OutboundServices;
using RiskGuard.Platform.Iam.Domain.Model.Aggregates;
using RiskGuard.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;

namespace RiskGuard.Platform.Iam.Infrastructure.Tokens.Jwt.Services;

public class TokenService(IOptions<TokenSettings> tokenSettings) : ITokenService
{
    private readonly TokenSettings _tokenSettings = tokenSettings.Value;

    public string GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        var claims = new[]
        {
            new Claim(ClaimTypes.Sid, user.Id),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, NormalizeRole(user.Role))
        };
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(handler.CreateToken(descriptor));
    }

    private static string NormalizeRole(string role)
    {
        return role.ToUpperInvariant() switch
        {
            "OPERATOR" or "OPERARIO" or "PLANT-OPERATOR" => "Operario",
            "SUPERVISOR" => "Supervisor",
            "MANAGER" or "ADMIN" or "ADMINISTRADOR" => "Administrador",
            _ => role
        };
    }
}
