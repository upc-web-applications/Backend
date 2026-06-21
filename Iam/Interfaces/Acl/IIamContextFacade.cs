namespace Acme.Center.Platform.Iam.Interfaces.Acl;

public interface IIamContextFacade
{
    Task<string?> FetchUsernameByUserId(string userId, CancellationToken cancellationToken = default);
    Task<bool> UserExistsAsync(string username, CancellationToken cancellationToken = default);
}
