namespace Vantage.Presentation.Hosting.Security
{
    /// <summary>
    /// Contains the constant string values for the custom claims used to enforce the 3D Matrix RBAC.
    /// </summary>
    public static class VantageClaims
    {
        #region Dimension 1: Tenant Context
        /// <summary>
        /// Represents the Organization Context. If present, the user belongs to an organization.
        /// </summary>
        public const string OrganizationId = "vantage_org_id";

        /// <summary>
        /// Represents the sub-tenant context. If present, the user is locked to a specific roster/team.
        /// </summary>
        public const string TeamId = "vantage_team_id";
        #endregion

        #region Dimension 3: Systemic Limits
        /// <summary>
        /// Represents the Access Level. Dictates the systemic limits and infrastructure routing (e.g., Basic, Pro, Custom).
        /// </summary>
        public const string SubscriptionTier = "vantage_tier";
        #endregion

        #region Dimension 2: Permissions
        /// <summary>
        /// Represents a granular functional permission. Multiple instances of this claim can exist in a single token.
        /// </summary>
        public const string Permission = "vantage_permission";
        #endregion
    }

    /// <summary>
    /// Contains the exhaustive constant string values for all granular system permissions across the platform.
    /// </summary>
    public static class VantagePermissions
    {
        #region Competitive & Roster
        /// <summary>Allows access to the platform's internal matchmaking queue.</summary>
        public const string MatchmakingInHouseQueue = "Matchmaking.InHouseQueue";

        /// <summary>Allows the organization to schedule scrimmages with other teams.</summary>
        public const string ScrimSchedule = "Scrim.Schedule";

        /// <summary>Allows the user to initiate a formal roster transfer request.</summary>
        public const string RosterTransferRequest = "Roster.TransferRequest";

        /// <summary>Allows the user to approve an incoming roster transfer request.</summary>
        public const string RosterTransferAccept = "Roster.TransferAccept";

        /// <summary>Allows standard management of internal roster configurations.</summary>
        public const string RosterManage = "Roster.Manage";
        #endregion

        #region Analytics & Intelligence
        /// <summary>Allows viewing of standard NPI metrics.</summary>
        public const string AnalyticsViewBasic = "Analytics.ViewBasic";

        /// <summary>Allows viewing of advanced visual analytics and heatmaps.</summary>
        public const string AnalyticsViewAdvanced = "Analytics.ViewAdvanced";

        /// <summary>Allows FA Staff to view in-depth tactician-level metrics for linked students.</summary>
        public const string AnalyticsViewTactician = "Analytics.ViewTactician";

        /// <summary>Allows FA Staff to view and compare group NPI metrics.</summary>
        public const string AnalyticsViewGroup = "Analytics.ViewGroup";

        /// <summary>Allows execution of high token depth AI inference requests.</summary>
        public const string AnalyticsHighTokenInference = "Analytics.HighTokenInference";

        /// <summary>Allows exporting of watermarked analytical data to external formats.</summary>
        public const string AnalyticsExportExcel = "Analytics.ExportExcel";

        /// <summary>Allows access to AI-powered scrimmage analysis.</summary>
        public const string AnalyticsScrimAnalysis = "Analytics.ScrimAnalysis";
        #endregion

        #region Financial & Organization
        /// <summary>Allows FA Staff to initiate secure financial holds for services rendered.</summary>
        public const string PaymentEscrow = "Payment.Escrow";

        /// <summary>Allows viewing of organizational financial and payment history.</summary>
        public const string BillingViewFinancials = "Billing.ViewFinancials";
        #endregion

        #region Tenant & Platform Management
        /// <summary>Grants foundational access to load the Blazor shell dashboard.</summary>
        public const string SystemDashboardAccess = "System.DashboardAccess";

        /// <summary>Allows direct Machine-to-Machine API access bypassing the UI.</summary>
        public const string SystemApiAccess = "System.ApiAccess";

        /// <summary>Allows linking an existing platform user to a staff dashboard.</summary>
        public const string TenantLinkStudent = "Tenant.LinkStudent";

        /// <summary>Allows a Partner to provision and fund a new platform account for a student.</summary>
        public const string TenantProvisionStudent = "Tenant.ProvisionStudent";

        /// <summary>Grants Level 1 visibility for self-promotion within the platform.</summary>
        public const string ProfileVisibilityL1 = "Profile.VisibilityL1";

        /// <summary>Grants Level 2 premium visibility for self-promotion within the platform.</summary>
        public const string ProfileVisibilityL2 = "Profile.VisibilityL2";

        /// <summary>Allows an organization to connect standard Discord integrations.</summary>
        public const string IntegrationDiscordBot = "Integration.DiscordBot";

        /// <summary>Allows an organization to apply custom branding to their Discord bot.</summary>
        public const string IntegrationDiscordBotBranding = "Integration.DiscordBotBranding";

        /// <summary>Allows enterprise organizations to configure white-label portal settings.</summary>
        public const string TenantWhiteLabel = "Tenant.WhiteLabel";

        /// <summary>Grants access to unreleased, experimental beta features.</summary>
        public const string SystemBetaAccess = "System.BetaAccess";
        #endregion
    }
}