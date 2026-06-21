using Acme.Center.Platform.Iam.Application.QueryServices;
using Acme.Center.Platform.Iam.Domain.Model.Queries;
using Acme.Center.Platform.Iam.Interfaces.Acl;

namespace Acme.Center.Platform.Iam.Application.Acl;

public class IamContextFacade(IUserQueryService userQueryService) : IIamContextFacade
{
    public async Task<string?> FetchUsernameByUserId(string userId, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByIdQuery(userId);
        var user = await userQueryService.Handle(query, cancellationToken);
        return user?.Username;
    }

    public async Task<bool> UserExistsAsync(string username, CancellationToken cancellationToken = default)
    {
        var query = new GetUserByUsernameQuery(username);
        var user = await userQueryService.Handle(query, cancellationToken);
        return user is not null;
    }
}
