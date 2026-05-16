using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Vantage.Presentation.Hosting.Security
{
    /// <summary>
    /// The marker requirement for endpoints that must be executed within the context of an Organization or Team.
    /// </summary>
    public class TenantRequirement : IAuthorizationRequirement
    {
    }

    /// <summary>
    /// Evaluates the TenantRequirement by ensuring the user's identity contains the appropriate Dimension 1 Tenant Claims.
    /// </summary>
    public class TenantAuthorizationHandler : AuthorizationHandler<TenantRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TenantRequirement requirement)
        {
            // Null safety: In test environments, the User or Identity might be entirely missing.
            if (context.User?.Identity == null)
            {
                return Task.CompletedTask; // Fails authorization gracefully instead of crashing
            }

            // Check if the user possesses an Organization ID or a Team ID claim
            var hasTenantContext = context.User.HasClaim(c =>
                c.Type == VantageClaims.OrganizationId ||
                c.Type == VantageClaims.TeamId);

            if (hasTenantContext)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}