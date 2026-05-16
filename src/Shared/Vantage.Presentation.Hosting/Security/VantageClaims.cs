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
    /// Contains the exhaustive list of granular system permissions.
    /// Premium permissions are injected dynamically via Tiers and are not granted by default base roles.
    /// </summary>
    public static class VantagePermissions
    {
        #region Competitive & Roster
        /// <summary>Access internal matchmaking queues.</summary>
        public const string MatchmakingInHouseQueue = "Matchmaking.InHouseQueue";

        /// <summary>Book practice matches against other teams.</summary>
        public const string ScrimSchedule = "Scrim.Schedule";

        /// <summary>Initiate a transfer request.</summary>
        public const string RosterTransferRequest = "Roster.TransferRequest";

        /// <summary>Approve an incoming player transfer.</summary>
        public const string RosterTransferAccept = "Roster.TransferAccept";

        /// <summary>Manage team slots, sub-ins, and lineup settings.</summary>
        public const string RosterManage = "Roster.Manage";
        #endregion

        #region Analytics & Intelligence
        /// <summary>View standard Anonymized Metrics.</summary>
        public const string AnalyticsViewBasic = "Analytics.ViewBasic";

        /// <summary>View advanced visual analytics and heatmaps.</summary>
        public const string AnalyticsViewAdvanced = "Analytics.ViewAdvanced";

        /// <summary>Access individual player/student profiles. Data resolution depends strictly on the viewer's Tier.</summary>
        public const string AnalyticsViewCoaching = "Analytics.ViewCoaching";

        /// <summary>Compare group metrics across a roster.</summary>
        public const string AnalyticsViewGroup = "Analytics.ViewGroup";

        /// <summary>Execute AI-powered scrimmage analysis.</summary>
        public const string AnalyticsScrimAnalysis = "Analytics.ScrimAnalysis";

        /// <summary>Execute high token depth AI requests.</summary>
        public const string AnalyticsHighTokenInference = "Analytics.HighTokenInference";

        /// <summary>Share and receive secure snapshots of dashboards internally.</summary>
        public const string AnalyticsShareInsights = "Analytics.ShareInsights";

        /// <summary>Export watermarked data to Excel (.xlsx) with backend audit logging.</summary>
        public const string AnalyticsExportExcel = "Analytics.ExportExcel";

        /// <summary>Export raw data to CSV with backend audit logging.</summary>
        public const string AnalyticsExportCsv = "Analytics.ExportCsv";

        /// <summary>Export watermarked formatted reports to PDF with backend audit logging.</summary>
        public const string AnalyticsExportPdf = "Analytics.ExportPdf";
        #endregion

        #region Financial & Organization
        /// <summary>Initiate financial holds for services rendered.</summary>
        public const string PaymentEscrow = "Payment.Escrow";

        /// <summary>View organizational financial and payment history.</summary>
        public const string BillingViewFinancials = "Billing.ViewFinancials";
        #endregion

        #region Tenant & Platform Management
        /// <summary>Base access to load the Blazor shell application.</summary>
        public const string SystemDashboardAccess = "System.DashboardAccess";

        /// <summary>Machine-to-Machine API bypass access.</summary>
        public const string SystemApiAccess = "System.ApiAccess";

        /// <summary>Access to unreleased experimental features.</summary>
        public const string SystemBetaAccess = "System.BetaAccess";

        /// <summary>Link an existing user to a staff dashboard.</summary>
        public const string TenantLinkStudent = "Tenant.LinkStudent";

        /// <summary>Provision and fund a new account for a student.</summary>
        public const string TenantProvisionStudent = "Tenant.ProvisionStudent";

        /// <summary>Configure white-label portal settings.</summary>
        public const string TenantWhiteLabel = "Tenant.WhiteLabel";

        /// <summary>Connect a standard Discord bot.</summary>
        public const string IntegrationDiscordBot = "Integration.DiscordBot";

        /// <summary>Apply custom branding to the Discord bot.</summary>
        public const string IntegrationDiscordBotBranding = "Integration.DiscordBotBranding";
        #endregion

        #region Ecosystem Visibility Matrices
        /// <summary>Prioritizes individual recruitment and content.</summary>
        public const string VisibilityUserL1 = "Visibility.User.L1";
        public const string VisibilityUserL2 = "Visibility.User.L2";
        public const string VisibilityUserL3 = "Visibility.User.L3";

        /// <summary>Prioritizes scrim queues and tournament seeding.</summary>
        public const string VisibilityTeamL1 = "Visibility.Team.L1";
        public const string VisibilityTeamL2 = "Visibility.Team.L2";
        public const string VisibilityTeamL3 = "Visibility.Team.L3";

        /// <summary>Prioritizes sponsorships and white-glove support.</summary>
        public const string VisibilityOrgL1 = "Visibility.Org.L1";
        public const string VisibilityOrgL2 = "Visibility.Org.L2";
        public const string VisibilityOrgL3 = "Visibility.Org.L3";
        #endregion

        #region Parent Aggregations (Flattened by IAM)
        /// <summary>PARENT: Unlocks Request and Accept transfer powers.</summary>
        public const string RosterManageTransfer = "Roster.ManageTransfer";

        /// <summary>PARENT: Unlocks Excel, CSV, and PDF exports.</summary>
        public const string AnalyticsExportAll = "Analytics.ExportAll";

        /// <summary>PARENT: Unlocks Basic, Advanced, Coaching, and Group views.</summary>
        public const string AnalyticsViewAll = "Analytics.ViewAll";
        #endregion
    }
}