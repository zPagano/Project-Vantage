using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Vantage.Presentation.Hosting.Localization
{
    /// <summary>
    /// Provides extension methods for configuring global internationalization (i18n) and localization routing.
    /// </summary>
    public static class LocalizationExtensions
    {
        /// <summary>
        /// Registers the localization services and configures the default resources directory.
        /// </summary>
        /// <param name="services">The service collection to add localization to.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddVantageLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            return services;
        }

        /// <summary>
        /// Inserts the request localization middleware into the HTTP pipeline, defining the strict matrix of supported cultures.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The modified application builder.</returns>
        public static IApplicationBuilder UseVantageRequestLocalization(this IApplicationBuilder app)
        {
            // ARCHITECTURE NOTE: EXPANDING CULTURES
            // To add a new language in the future (e.g., Korean "ko-KR"):
            // Add the culture code to the array below.
            // Create a matching .resx file in the Resources folder (e.g., Localization.SharedResource.ko-KR.resx).
            // The framework will automatically begin parsing Accept-Language headers for the new code.

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"), // Default Fallback
                new CultureInfo("pt-BR"),
                new CultureInfo("es"),
                new CultureInfo("fr")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);

            return app;
        }
    }
}