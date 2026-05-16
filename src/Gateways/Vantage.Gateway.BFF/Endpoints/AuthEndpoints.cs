using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Vantage.Presentation.Hosting.Security;

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

            #region Simulated Login Routes

            group.MapPost("/login/manager", async (HttpContext context) =>
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "manager-123"),
                    new Claim(ClaimTypes.Name, "Demo Manager"),
                    // Dimension 1: Tenant
                    new Claim(VantageClaims.OrganizationId, "org-black-bulls"),
                    // Dimension 3: Tier
                    new Claim(VantageClaims.SubscriptionTier, "Pro"),
                    // Dimension 2: Permissions
                    new Claim(VantageClaims.Permission, VantagePermissions.RosterManage),
                    new Claim(VantageClaims.Permission, VantagePermissions.ScrimSchedule)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return Results.Ok(new { Message = "Logged in as Organization Manager." });
            });

            group.MapPost("/login/freeagent", async (HttpContext context) =>
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "fa-coach-456"),
                    new Claim(ClaimTypes.Name, "Demo FA Coach"),
                    // Dimension 1: Missing OrganizationId (Implies Free Agent)
                    // Dimension 3: Tier
                    new Claim(VantageClaims.SubscriptionTier, "Partner"),
                    // Dimension 2: Permissions
                    new Claim(VantageClaims.Permission, VantagePermissions.PaymentEscrow),
                    new Claim(VantageClaims.Permission, VantagePermissions.TenantProvisionStudent)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                return Results.Ok(new { Message = "Logged in as Free Agent Staff." });
            });

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

            #endregion

            #region Protected Test Routes

            // This endpoint requires BOTH the Organization Context AND the Roster Manage Permission
            group.MapGet("/test/roster", () => Results.Ok(new { Message = "Roster management access granted." }))
                 .RequireAuthorization("Context.Organization", VantagePermissions.RosterManage);

            // This endpoint requires BOTH the Free Agent Context AND the Escrow Permission
            group.MapGet("/test/escrow", () => Results.Ok(new { Message = "Financial escrow access granted." }))
                 .RequireAuthorization("Context.FreeAgent", VantagePermissions.PaymentEscrow);

            // Diagnostic endpoint to check current session state
            group.MapGet("/user", (ClaimsPrincipal user) =>
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

            #endregion

            return app;
        }
    }
}
