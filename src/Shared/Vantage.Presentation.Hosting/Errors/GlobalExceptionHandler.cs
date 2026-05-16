using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Vantage.Presentation.Hosting.Localization;

namespace Vantage.Presentation.Hosting.Errors
{
    /// <summary>
    /// intercepts unhandled exceptions globally and formats them into RFC 9457 compliant, localized ProblemDetails responses.
    /// </summary>
    public sealed class GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IStringLocalizer<SharedResource> localizer) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Log the full exception with OpenTelemetry-compatible structured logging
            logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            // Restore the culture from the HTTP Context before translating.
            // When exceptions bubble up the pipeline, the AsyncLocal context unwinds and resets the thread culture.
            var requestCultureFeature = httpContext.Features.Get<IRequestCultureFeature>();
            if (requestCultureFeature != null)
            {
                CultureInfo.CurrentCulture = requestCultureFeature.RequestCulture.Culture;
                CultureInfo.CurrentUICulture = requestCultureFeature.RequestCulture.UICulture;
            }

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = localizer["ServerErrorTitle"],
                Type = "https://datatracker.ietf.org/doc/html/rfc9457",
                Instance = httpContext.Request.Path,
                Detail = localizer["ServerErrorDetail"]
            };

            // Specific mapping for security/boundary violations
            if (exception is UnauthorizedAccessException)
            {
                problemDetails.Status = StatusCodes.Status401Unauthorized;
                problemDetails.Title = localizer["UnauthorizedTitle"];
                problemDetails.Detail = localizer["UnauthorizedDetail"];
            }
            else if (exception is ValidationException validationException)
            {
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = localizer["ValidationFailedTitle"];
                problemDetails.Detail = localizer["ValidationFailedDetail"];

                // Map the FluentValidation errors directly into the ProblemDetails extensions
                problemDetails.Extensions["errors"] = validationException.Errors
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            // Return true to signal that this exception has been handled
            return true;
        }
    }
}