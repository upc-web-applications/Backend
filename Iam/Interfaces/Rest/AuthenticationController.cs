using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RiskGuard.Platform.Iam.Application.Internal.Services;
using RiskGuard.Platform.Iam.Domain.Model.Commands;
using RiskGuard.Platform.Iam.Interfaces.Rest.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace RiskGuard.Platform.Iam.Interfaces.Rest;

[ApiController]
[Route("api/v1/authentication")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Authentication endpoints for RiskGuard users.")]
public class AuthenticationController(IamCommandService iamCommandService) : ControllerBase
{
    [HttpPost("sign-in")]
    [SwaggerOperation("Sign in", "Authenticate a user and return a JWT token.")]
    public async Task<IActionResult> SignIn(SignInResource resource, CancellationToken cancellationToken)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var result = await iamCommandService.Handle(new SignInCommand(resource.Email, resource.Password), ipAddress,
            cancellationToken);
        if (result.IsFailure) return BadRequest(new { message = result.Message, error = result.Error });
        var (user, token) = result.Value;
        return Ok(new AuthenticatedUserResource(user.Id, user.Name, user.Email, user.Role, user.AccountStatus, token));
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up", "Create a user account.")]
    public async Task<IActionResult> SignUp(SignUpResource resource, CancellationToken cancellationToken)
    {
        var result = await iamCommandService.Handle(
            new SignUpCommand(resource.Name, resource.Email, resource.Password, resource.Role, resource.SiteAreaId),
            cancellationToken);
        return result.IsFailure ? BadRequest(new { message = result.Message, error = result.Error }) : Ok(result.Value);
    }
}
