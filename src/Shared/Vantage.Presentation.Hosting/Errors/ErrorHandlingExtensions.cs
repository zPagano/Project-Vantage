using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Vantage.Presentation.Hosting.Errors
{
    public static class ErrorHandlingExtensions
    {
        public static IServiceCollection AddVantageErrorHandling(this IServiceCollection services)
        {
            // Registers the RFC 9457 Problem Details service
            services.AddProblemDetails();

            // Registers the specific handler you built in GlobalExceptionHandler.cs
            services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }

        public static IApplicationBuilder UseVantageErrorHandling(this IApplicationBuilder app)
        {
            // Inserts the exception handling middleware into the HTTP pipeline
            app.UseExceptionHandler();

            return app;
        }
    }
}
