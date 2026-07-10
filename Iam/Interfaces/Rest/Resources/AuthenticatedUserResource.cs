namespace Acme.Center.Platform.Iam.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(string Id, string Username, string Email, string Name, string Role, string Token);
