using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Vantage.Gateway.BFF.Endpoints
{
    /// <summary>
    /// Provides extension methods for mapping authentication-related endpoints in the Backend-For-Frontend (BFF) gateway.
    /// </summary>
    public static class AuthEndpoints
    {
        /// <summary>
        /// Maps the login, logout, and user session diagnostic endpoints to the provided route builder.
        /// </summary>
        /// <param name="app">The endpoint route builder to which the authentication endpoints are added.</param>
        /// <returns>The endpoint route builder with the authentication endpoints configured.</returns>
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/login", async (HttpContext context) =>
            {
                // TODO (Phase 2): Forward credentials to the Identity Provider to get a real JWT.
                // For now, we simulate a successful login to prove the HttpOnly cookie is generated.
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "simulated-manager-123"),
                    new Claim(ClaimTypes.Name, "DemoManager"),
                    new Claim(ClaimTypes.Role, "Manager")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // This generates the encrypted __Host-Vantage-Session cookie
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Results.Ok(new { Message = "Logged in successfully." });
            });

            group.MapPost("/logout", async (HttpContext context) =>
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Results.Ok(new { Message = "Logged out successfully." });
            });

            // Diagnostic endpoint to check current session state
            group.MapPost("/user", (ClaimsPrincipal user) =>
            {
                if (user.Identity?.IsAuthenticated == true)
                {
                    return Results.Ok(new
                    {
                        Id = user.FindFirstValue(ClaimTypes.NameIdentifier),
                        Name = user.Identity?.Name,
                        Role = user.FindFirstValue(ClaimTypes.Role)
                    });
                }
                return Results.Unauthorized();
            });

            return app;
        }
    }
}
