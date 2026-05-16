using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Vantage.Presentation.Hosting.Security
{
    /// <summary>
    /// Extension methods for configuring domain-specific authorization policies across the distributed system.
    /// </summary>
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddVantageAuthorization(this IServiceCollection services)
        {
            // Core DI requirements for the 3D Matrix to evaluate Tenant contexts
            services.AddSingleton<IAuthorizationHandler, TenantAuthorizationHandler>();

            services.AddAuthorization(options =>
            {
                // ==========================================
                // TENANT-BOUND POLICIES (Requires Org/Team Context)
                // ==========================================

                options.AddPolicy(VantagePermissions.ScrimSchedule, p => p
                    .RequireClaim(VantageClaims.Permission, VantagePermissions.ScrimSchedule)
                    .AddRequirements(new TenantRequirement()));

                options.AddPolicy(VantagePermissions.RosterTransferAccept, p => p
                    .RequireClaim(VantageClaims.Permission, VantagePermissions.RosterTransferAccept)
                    .AddRequirements(new TenantRequirement()));

                options.AddPolicy(VantagePermissions.RosterManage, p => p
                    .RequireClaim(VantageClaims.Permission, VantagePermissions.RosterManage)
                    .AddRequirements(new TenantRequirement()));

                options.AddPolicy(VantagePermissions.AnalyticsViewGroup, p => p
                    .RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsViewGroup)
                    .AddRequirements(new TenantRequirement()));

                options.AddPolicy(VantagePermissions.AnalyticsShareInsights, p => p
                    .RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsShareInsights)
                    .AddRequirements(new TenantRequirement()));

                options.AddPolicy(VantagePermissions.IntegrationDiscordBot, p => p
                    .RequireClaim(VantageClaims.Permission, VantagePermissions.IntegrationDiscordBot)
                    .AddRequirements(new TenantRequirement()));

                // ==========================================
                // GLOBAL POLICIES (No specific Tenant Context required)
                // ==========================================

                // Competitive & Roster
                options.AddPolicy(VantagePermissions.MatchmakingInHouseQueue, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.MatchmakingInHouseQueue));
                options.AddPolicy(VantagePermissions.RosterTransferRequest, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.RosterTransferRequest));

                // Analytics & Intelligence
                options.AddPolicy(VantagePermissions.AnalyticsViewBasic, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsViewBasic));
                options.AddPolicy(VantagePermissions.AnalyticsViewAdvanced, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsViewAdvanced));
                options.AddPolicy(VantagePermissions.AnalyticsViewCoaching, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsViewCoaching));
                options.AddPolicy(VantagePermissions.AnalyticsScrimAnalysis, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsScrimAnalysis));
                options.AddPolicy(VantagePermissions.AnalyticsHighTokenInference, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsHighTokenInference));
                options.AddPolicy(VantagePermissions.AnalyticsExportExcel, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsExportExcel));
                options.AddPolicy(VantagePermissions.AnalyticsExportCsv, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsExportCsv));
                options.AddPolicy(VantagePermissions.AnalyticsExportPdf, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.AnalyticsExportPdf));

                // Financial & Organization
                options.AddPolicy(VantagePermissions.PaymentEscrow, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.PaymentEscrow));
                options.AddPolicy(VantagePermissions.BillingViewFinancials, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.BillingViewFinancials));

                // Tenant & Platform Management
                options.AddPolicy(VantagePermissions.SystemDashboardAccess, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.SystemDashboardAccess));
                options.AddPolicy(VantagePermissions.SystemApiAccess, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.SystemApiAccess));
                options.AddPolicy(VantagePermissions.SystemBetaAccess, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.SystemBetaAccess));
                options.AddPolicy(VantagePermissions.TenantLinkStudent, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.TenantLinkStudent));
                options.AddPolicy(VantagePermissions.TenantProvisionStudent, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.TenantProvisionStudent));
                options.AddPolicy(VantagePermissions.TenantWhiteLabel, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.TenantWhiteLabel));
                options.AddPolicy(VantagePermissions.IntegrationDiscordBotBranding, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.IntegrationDiscordBotBranding));

                // Ecosystem Visibility Matrices
                options.AddPolicy(VantagePermissions.VisibilityUserL1, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityUserL1));
                options.AddPolicy(VantagePermissions.VisibilityUserL2, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityUserL2));
                options.AddPolicy(VantagePermissions.VisibilityUserL3, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityUserL3));

                options.AddPolicy(VantagePermissions.VisibilityTeamL1, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityTeamL1));
                options.AddPolicy(VantagePermissions.VisibilityTeamL2, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityTeamL2));
                options.AddPolicy(VantagePermissions.VisibilityTeamL3, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityTeamL3));

                options.AddPolicy(VantagePermissions.VisibilityOrgL1, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityOrgL1));
                options.AddPolicy(VantagePermissions.VisibilityOrgL2, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityOrgL2));
                options.AddPolicy(VantagePermissions.VisibilityOrgL3, p => p.RequireClaim(VantageClaims.Permission, VantagePermissions.VisibilityOrgL3));
            });

            return services;
        }
    }
}