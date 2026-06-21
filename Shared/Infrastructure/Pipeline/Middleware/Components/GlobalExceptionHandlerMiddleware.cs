using System.Net;
using System.Text.Json;

namespace RiskGuard.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled RiskGuard backend exception");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";
            var payload = new
            {
                title = "Internal server error",
                status = context.Response.StatusCode,
                detail = exception.Message,
                instance = context.Request.Path.Value
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}
