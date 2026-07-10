using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Acme.Center.Platform.Iam.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Interfaces.Rest;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.Iam.Interfaces.Rest;

[ApiController]
[Authorize]
[Route("api/v1/roles")]
[Produces("application/json")]
[SwaggerTag("Role Endpoints")]
public class RolesController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Role>(context, unitOfWork);

[ApiController]
[Authorize]
[Route("api/v1/sessions")]
[Produces("application/json")]
[SwaggerTag("Session Endpoints")]
public class SessionsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<Session>(context, unitOfWork);

[ApiController]
[Authorize]
[Route("api/v1/access-logs")]
[Produces("application/json")]
[SwaggerTag("Access Log Endpoints")]
public class AccessLogsController(AppDbContext context, IUnitOfWork unitOfWork)
    : CrudController<AccessLog>(context, unitOfWork);
