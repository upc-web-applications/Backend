using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Acme.Center.Platform.Iam.Application.QueryServices;
using Acme.Center.Platform.Iam.Domain.Model.Queries;
using Acme.Center.Platform.Iam.Interfaces.Rest.Resources;
using Acme.Center.Platform.Iam.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/users")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("User Endpoints")]
[Authorize]
public class UsersController(IUserQueryService userQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all users")]
    [SwaggerResponse(200, "The users were found.", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllUsersQuery();
        var users = await userQueryService.Handle(query, cancellationToken);
        var resources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get user by id")]
    [SwaggerResponse(200, "The user was found.", typeof(UserResource))]
    [SwaggerResponse(404, "The user was not found.")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(query, cancellationToken);
        if (user is null) return NotFound();
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(user));
    }
}
