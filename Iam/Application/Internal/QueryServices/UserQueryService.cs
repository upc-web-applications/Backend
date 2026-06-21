using Acme.Center.Platform.Iam.Application.QueryServices;
using Acme.Center.Platform.Iam.Domain.Model.Aggregates;
using Acme.Center.Platform.Iam.Domain.Model.Queries;
using Acme.Center.Platform.Iam.Domain.Repositories;

namespace Acme.Center.Platform.Iam.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        => await userRepository.ListAsync(cancellationToken);

    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        => await userRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<User?> Handle(GetUserByUsernameQuery query, CancellationToken cancellationToken)
        => await userRepository.FindByUsernameAsync(query.Username, cancellationToken);
}
