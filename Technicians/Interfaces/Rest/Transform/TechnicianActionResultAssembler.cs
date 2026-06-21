using Acme.Center.Platform.Technicians.Domain.Model;
using Acme.Center.Platform.Technicians.Domain.Model.Aggregates;
using Acme.Center.Platform.Shared.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Center.Platform.Technicians.Interfaces.Rest.Transform;

public static class TechnicianActionResultAssembler
{
    public static IActionResult ToActionResultFromCreateResult(
        ControllerBase controller,
        Result<Technician> result,
        Func<Technician, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);
        var statusCode = result.Error switch
        {
            TechnicianError.TechnicianNotFound => 404,
            TechnicianError.DuplicateDocumentNumber => 409,
            TechnicianError.OperationCancelled => 409,
            TechnicianError.DatabaseError => 500,
            TechnicianError.InternalServerError => 500,
            _ => 400
        };
        return controller.StatusCode(statusCode, new { error = result.Error!.ToString(), message = result.Message });
    }

    public static IActionResult ToActionResultFromGetByIdResult(
        ControllerBase controller,
        Technician? entity,
        Func<Technician, IActionResult> successAction)
    {
        if (entity is null)
            return controller.NotFound(new { error = TechnicianError.TechnicianNotFound.ToString(), message = "Technician not found." });
        return successAction(entity);
    }
}
