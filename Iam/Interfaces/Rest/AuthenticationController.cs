using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Acme.Center.Platform.Iam.Application.CommandServices;
using Acme.Center.Platform.Iam.Interfaces.Rest.Resources;
using Acme.Center.Platform.Iam.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Acme.Center.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Authentication Endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in", "Authenticate a user and return a JWT token.")]
    [SwaggerResponse(200, "Authentication successful.", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(401, "Invalid credentials.")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource, CancellationToken cancellationToken)
    {
        var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(command, cancellationToken);
        return IamActionResultAssembler.ToActionResultFromSignInResult(
            this, result,
            uat => Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(uat.user, uat.token)));
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up", "Register a new user.")]
    [SwaggerResponse(200, "User created successfully.")]
    [SwaggerResponse(409, "Username already taken.")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource, CancellationToken cancellationToken)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(command, cancellationToken);
        return IamActionResultAssembler.ToActionResultFromSignUpResult(
            this, result,
            () => Ok(new { message = "User created successfully." }));
    }
}
