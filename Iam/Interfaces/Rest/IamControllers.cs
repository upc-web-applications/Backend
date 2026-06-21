using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiskGuard.Platform.Iam.Domain.Model.Aggregates;
using RiskGuard.Platform.Shared.Domain.Repositories;
using RiskGuard.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using RiskGuard.Platform.Shared.Interfaces.Rest;

namespace RiskGuard.Platform.Iam.Interfaces.Rest;

[Route("api/v1/users")]
public class UsersController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<User>(context, unitOfWork)
{
    [HttpPut("{id}")]
    public override async Task<IActionResult> Update(string id, [FromBody] User resource,
        CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (user is null) return NotFound();

        if (!string.IsNullOrWhiteSpace(resource.Name)) user.Name = resource.Name;
        if (!string.IsNullOrWhiteSpace(resource.Email)) user.Email = resource.Email;
        if (!string.IsNullOrWhiteSpace(resource.RoleId)) user.RoleId = resource.RoleId;
        if (resource.SectorId is not null) user.SectorId = resource.SectorId;
        if (!string.IsNullOrWhiteSpace(resource.AccountStatus)) user.AccountStatus = resource.AccountStatus;
        user.FailedAttempts = resource.FailedAttempts;
        user.LockedUntil = resource.LockedUntil;
        if (!string.IsNullOrWhiteSpace(resource.Password)) user.PasswordHash = resource.Password;

        await unitOfWork.CompleteAsync(cancellationToken);
        return Ok(user);
    }
}

[Route("api/v1/roles")]
public class RolesController(AppDbContext context, IUnitOfWork unitOfWork) : CrudController<Role>(context, unitOfWork);

[Route("api/v1/sessions")]
public class SessionsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Session>(context, unitOfWork);

[Route("api/v1/accessLogs")]
public class AccessLogsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<AccessLog>(context, unitOfWork);
