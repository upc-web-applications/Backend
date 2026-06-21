using Acme.Center.Platform.Hazards.Domain.Model;
using Acme.Center.Platform.Hazards.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Center.Platform.Hazards.Interfaces.Rest.Transform;

public static class HazardActionResultAssembler
{
    public static IActionResult ToActionResultFromCreateResult(
        ControllerBase controller,
        Result<Hazard> result,
        Func<Hazard, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = result.Error switch
        {
            HazardError.HazardNotFound => 404,
            HazardError.DuplicateHazardCode => 409,
            HazardError.OperationCancelled => 409,
            HazardError.DatabaseError => 500,
            HazardError.InternalServerError => 500,
            _ => 400
        };
        return controller.StatusCode(statusCode, new { error = result.Error!.ToString(), message = result.Message });
    }

    public static IActionResult ToActionResultFromGetByIdResult(
        ControllerBase controller,
        Hazard? entity,
        Func<Hazard, IActionResult> successAction)
    {
        if (entity is null)
            return controller.NotFound(new { error = HazardError.HazardNotFound.ToString(), message = "Hazard not found." });
        return successAction(entity);
    }
}
