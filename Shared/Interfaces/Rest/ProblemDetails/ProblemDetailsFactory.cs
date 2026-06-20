using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RiskGuard.Platform.Shared.Resources;
using RiskGuard.Platform.Shared.Resources.Errors;

namespace RiskGuard.Platform.Shared.Interfaces.Rest.ProblemDetails;

public class ProblemDetailsFactory(
    IStringLocalizer<ErrorMessage> errorLocalizer,
    IStringLocalizer<CommonMessages> commonLocalizer,
    Microsoft.AspNetCore.Mvc.Infrastructure.ProblemDetailsFactory aspNetCoreProblemDetailsFactory)
{
    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        Enum? errorEnum,
        string detailMessage)
    {
        var problemDetails = aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            errorEnum != null ? errorLocalizer[$"{errorEnum}"] : commonLocalizer["GenericError"],
            detail: detailMessage
        );

        problemDetails.Title = errorEnum != null ? errorLocalizer[$"{errorEnum}"] : commonLocalizer["GenericError"];
        problemDetails.Detail = detailMessage;
        problemDetails.Instance = controller.HttpContext.Request.Path;

        return controller.StatusCode(statusCode, problemDetails);
    }

    public IActionResult CreateProblemDetails(
        ControllerBase controller,
        int statusCode,
        string titleKey,
        string detailKey,
        params object[] detailArgs)
    {
        var problemDetails = aspNetCoreProblemDetailsFactory.CreateProblemDetails(
            controller.HttpContext,
            statusCode,
            commonLocalizer[titleKey],
            detail: errorLocalizer[detailKey, detailArgs],
            instance: controller.HttpContext.Request.Path
        );
        return controller.StatusCode(statusCode, problemDetails);
    }
}
