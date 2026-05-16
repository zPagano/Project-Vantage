using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Vantage.Presentation.Hosting.Security
{
    /// <summary>
    /// Provides extension methods for configuring the 3D Matrix Role-Based Access Control policies using modern decoupled mapping.
    /// </summary>
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// Registers the exhaustive Vantage authorization policies using the AuthorizationBuilder.
        /// </summary>
        /// <param name="services">The service collection to add the authorization policies to.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddVantageAuthorizationPolicies(this IServiceCollection services)
        {
            var builder = services.AddAuthorizationBuilder();

            #region Dimension 1: Context Policies

            // Requires the user to belong to an Organization
            builder.AddPolicy("Context.Organization", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(VantageClaims.OrganizationId);
            });

            // Requires the user to act as an independent Free Agent (No Organization)
            builder.AddPolicy("Context.FreeAgent", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context => !context.User.HasClaim(c => c.Type == VantageClaims.OrganizationId));
            });

            #endregion

            #region Dimension 2: Granular Permission Policies

            #region Competitive & Roster
            builder.AddPolicy(VantagePermissions.MatchmakingInHouseQueue, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.MatchmakingInHouseQueue));
            builder.AddPolicy(VantagePermissions.ScrimSchedule, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.ScrimSchedule));
            builder.AddPolicy(VantagePermissions.RosterTransferRequest, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.RosterTransferRequest));
            builder.AddPolicy(VantagePermissions.RosterTransferAccept, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.RosterTransferAccept));
            builder.AddPolicy(VantagePermissions.RosterManage, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.RosterManage));
            #endregion

            #region Analytics & Intelligence
            builder.AddPolicy(VantagePermissions.AnalyticsViewBasic, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsViewBasic));
            builder.AddPolicy(VantagePermissions.AnalyticsViewAdvanced, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsViewAdvanced));
            builder.AddPolicy(VantagePermissions.AnalyticsViewTactician, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsViewTactician));
            builder.AddPolicy(VantagePermissions.AnalyticsViewGroup, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsViewGroup));
            builder.AddPolicy(VantagePermissions.AnalyticsHighTokenInference, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsHighTokenInference));
            builder.AddPolicy(VantagePermissions.AnalyticsExportExcel, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsExportExcel));
            builder.AddPolicy(VantagePermissions.AnalyticsScrimAnalysis, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsScrimAnalysis));
            #endregion

            #region Financial & Organization
            builder.AddPolicy(VantagePermissions.PaymentEscrow, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.PaymentEscrow));
            builder.AddPolicy(VantagePermissions.BillingViewFinancials, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.BillingViewFinancials));
            #endregion

            #region Tenant & Platform Management
            builder.AddPolicy(VantagePermissions.SystemDashboardAccess, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.SystemDashboardAccess));
            builder.AddPolicy(VantagePermissions.SystemApiAccess, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.SystemApiAccess));
            builder.AddPolicy(VantagePermissions.TenantLinkStudent, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.TenantLinkStudent));
            builder.AddPolicy(VantagePermissions.TenantProvisionStudent, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.TenantProvisionStudent));
            builder.AddPolicy(VantagePermissions.ProfileVisibilityL1, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.ProfileVisibilityL1));
            builder.AddPolicy(VantagePermissions.ProfileVisibilityL2, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.ProfileVisibilityL2));
            builder.AddPolicy(VantagePermissions.IntegrationDiscordBot, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.IntegrationDiscordBot));
            builder.AddPolicy(VantagePermissions.IntegrationDiscordBotBranding, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.IntegrationDiscordBotBranding));
            builder.AddPolicy(VantagePermissions.TenantWhiteLabel, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.TenantWhiteLabel));
            builder.AddPolicy(VantagePermissions.SystemBetaAccess, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.SystemBetaAccess));
            #endregion

            #endregion

            return services;
        }
    }
}