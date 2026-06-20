using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RiskGuard.Platform.Shared.Resources;
using RiskGuard.Platform.Shared.Resources.Errors;

namespace RiskGuard.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;

public class GlobalExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlerMiddleware> logger,
    IStringLocalizer<ErrorMessage> errorLocalizer,
    IStringLocalizer<CommonMessages> commonLocalizer)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "Request was cancelled: {Message}", ex.Message);
            await HandleOperationCanceledExceptionAsync(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleGenericExceptionAsync(httpContext);
        }
    }

    private async Task HandleOperationCanceledExceptionAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = errorLocalizer["OperationCancelled"],
            Detail = errorLocalizer["OperationCancelled"],
            Instance = httpContext.Request.Path
        };

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, jsonOptions));
    }

    private async Task HandleGenericExceptionAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = commonLocalizer["InternalServerError"],
            Detail = errorLocalizer["GenericError"],
            Instance = httpContext.Request.Path
        };

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails, jsonOptions));
    }
}
