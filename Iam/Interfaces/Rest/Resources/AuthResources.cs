using System.Text.Json.Serialization;

namespace RiskGuard.Platform.Iam.Interfaces.Rest.Resources;

public record SignInResource(string Email, string Password);

public record SignUpResource(string Name, string Email, string Password, string Role, string? SiteAreaId);

public record AuthenticatedUserResource(
    string Id,
    string Name,
    string Email,
    string Role,
    [property: JsonPropertyName("account_status")]
    string AccountStatus,
    string Token);
