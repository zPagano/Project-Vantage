using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Vantage.Presentation.Hosting.Errors
{
    public sealed class GlobalExceptionHandler (ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Log the full exception with OpenTelemetry-compatible structured logging
            logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Type = "https://datatracker.ietf.org/doc/html/rfc9457",
                Instance = httpContext.Request.Path,
                Detail = "An unexpected error occurred. Please refer to the trace ID for support."
            };

            // Specific mapping for security/boundary violations
            if (exception is UnauthorizedAccessException)
            {
                problemDetails.Status = StatusCodes.Status401Unauthorized;
                problemDetails.Title = "Unauthorized";
                problemDetails.Detail = "Access is denied due to invalid or missing credentials.";
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            // Return true to signal that this exception has been handled
            return true;
        }
    }
}
