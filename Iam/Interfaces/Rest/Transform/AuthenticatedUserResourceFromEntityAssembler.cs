using Acme.Center.Platform.Iam.Domain.Model.Aggregates;
using Acme.Center.Platform.Iam.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
        => new(user.Id, user.Username, token);
}
