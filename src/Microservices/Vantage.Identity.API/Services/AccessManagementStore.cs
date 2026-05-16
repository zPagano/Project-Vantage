using System.Collections.Generic;
using System.Linq;
using Vantage.Presentation.Hosting.Security;

namespace Vantage.Identity.API.Services
{
    #region Models

    public class RoleDefinition
    {
        public string Name { get; set; } = string.Empty;
        public HashSet<string> Permissions { get; set; } = new();
    }

    #endregion

    #region Interfaces

    public interface IAccessManagementStore
    {
        IEnumerable<string> GetPermissionsForRoles(IEnumerable<string> roleNames);
        IDictionary<string, string[]> GetPermissionHierarchy();
    }

    #endregion

    #region Implementations

    public class InMemoryAccessManagementStore : IAccessManagementStore
    {
        private readonly List<RoleDefinition> _roles;
        private readonly Dictionary<string, string[]> _hierarchy;

        public InMemoryAccessManagementStore()
        {
            // Map the Parent permissions to their atomic Children
            _hierarchy = new Dictionary<string, string[]>
            {
                {
                    VantagePermissions.RosterManageTransfer,
                    new[] { VantagePermissions.RosterTransferRequest, VantagePermissions.RosterTransferAccept }
                },
                {
                    VantagePermissions.AnalyticsExportAll,
                    new[] { VantagePermissions.AnalyticsExportExcel, VantagePermissions.AnalyticsExportCsv, VantagePermissions.AnalyticsExportPdf }
                },
                {
                    VantagePermissions.AnalyticsViewAll,
                    new[] { VantagePermissions.AnalyticsViewBasic, VantagePermissions.AnalyticsViewAdvanced, VantagePermissions.AnalyticsViewCoaching, VantagePermissions.AnalyticsViewGroup }
                }
            };

            // Exhaustive Base Roles (Stripped of Premium/Injected Claims)
            _roles = new List<RoleDefinition>
            {
                new RoleDefinition
                {
                    Name = "System Administrator",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.SystemApiAccess,
                        VantagePermissions.AnalyticsViewAll,
                        VantagePermissions.AnalyticsExportAll
                    }
                },
                new RoleDefinition
                {
                    Name = "Organization Owner",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.RosterManage,
                        VantagePermissions.RosterManageTransfer,
                        VantagePermissions.BillingViewFinancials,
                        VantagePermissions.IntegrationDiscordBot,
                        VantagePermissions.AnalyticsViewGroup,
                        VantagePermissions.AnalyticsShareInsights
                    }
                },
                new RoleDefinition
                {
                    Name = "Organization Manager",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.RosterManage,
                        VantagePermissions.RosterManageTransfer,
                        VantagePermissions.ScrimSchedule,
                        VantagePermissions.IntegrationDiscordBot,
                        VantagePermissions.AnalyticsShareInsights,
                        VantagePermissions.AnalyticsViewGroup,
                        VantagePermissions.AnalyticsViewCoaching
                    }
                },
                new RoleDefinition
                {
                    Name = "Head Coach",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.RosterManage,
                        VantagePermissions.RosterManageTransfer,
                        VantagePermissions.ScrimSchedule,
                        VantagePermissions.AnalyticsViewCoaching,
                        VantagePermissions.AnalyticsViewGroup,
                        VantagePermissions.AnalyticsShareInsights,
                        VantagePermissions.AnalyticsExportAll
                    }
                },
                new RoleDefinition
                {
                    Name = "Assistant Coach",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.ScrimSchedule,
                        VantagePermissions.RosterTransferRequest,
                        VantagePermissions.AnalyticsViewCoaching,
                        VantagePermissions.AnalyticsShareInsights,
                        VantagePermissions.AnalyticsViewGroup
                    }
                },
                new RoleDefinition
                {
                    Name = "Positional Coach",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.AnalyticsViewCoaching,
                        VantagePermissions.AnalyticsShareInsights
                    }
                },
                new RoleDefinition
                {
                    Name = "Analyst",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.AnalyticsViewGroup,
                        VantagePermissions.AnalyticsShareInsights,
                        VantagePermissions.AnalyticsViewCoaching,
                        VantagePermissions.AnalyticsExportAll
                    }
                },
                new RoleDefinition
                {
                    Name = "Player",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.MatchmakingInHouseQueue,
                        VantagePermissions.AnalyticsViewBasic
                    }
                },
                new RoleDefinition
                {
                    Name = "Free Agent Player",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.MatchmakingInHouseQueue,
                        VantagePermissions.RosterTransferRequest,
                        VantagePermissions.AnalyticsViewBasic
                    }
                },
                new RoleDefinition
                {
                    Name = "Free Agent Staff",
                    Permissions = new HashSet<string>
                    {
                        VantagePermissions.SystemDashboardAccess,
                        VantagePermissions.PaymentEscrow,
                        VantagePermissions.TenantLinkStudent,
                        VantagePermissions.AnalyticsViewCoaching,
                        VantagePermissions.AnalyticsShareInsights
                    }
                }
            };
        }

        public IEnumerable<string> GetPermissionsForRoles(IEnumerable<string> roleNames)
        {
            return _roles
                .Where(r => roleNames.Contains(r.Name))
                .SelectMany(r => r.Permissions)
                .Distinct();
        }

        public IDictionary<string, string[]> GetPermissionHierarchy()
        {
            return _hierarchy;
        }
    }

    #endregion
}