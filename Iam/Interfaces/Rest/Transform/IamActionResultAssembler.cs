using Acme.Center.Platform.Iam.Domain.Model;
using Acme.Center.Platform.Iam.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Center.Platform.Iam.Interfaces.Rest.Transform;

public static class IamActionResultAssembler
{
    public static IActionResult ToActionResultFromSignInResult(
        ControllerBase controller,
        Result<(User user, string token)> result,
        Func<(User user, string token), IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = (IamError)result.Error! switch
        {
            IamError.InvalidCredentials => 401,
            IamError.OperationCancelled => 409,
            IamError.DatabaseError => 500,
            IamError.InternalServerError => 500,
            _ => 400
        };
        return controller.StatusCode(statusCode, new { error = result.Error!.ToString(), message = result.Message });
    }

    public static IActionResult ToActionResultFromSignUpResult(
        ControllerBase controller,
        Result result,
        Func<IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction();
        var statusCode = (IamError)result.Error! switch
        {
            IamError.UsernameAlreadyTaken => 409,
            IamError.OperationCancelled => 409,
            IamError.DatabaseError => 500,
            IamError.InternalServerError => 500,
            _ => 400
        };
        return controller.StatusCode(statusCode, new { error = result.Error!.ToString(), message = result.Message });
    }
}
