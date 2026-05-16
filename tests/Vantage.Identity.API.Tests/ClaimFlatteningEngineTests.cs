using System.Collections.Generic;
using System.Linq;
using Vantage.Identity.API.Services;
using Vantage.Presentation.Hosting.Security;
using Xunit;

namespace Vantage.Identity.API.Tests
{
    /// <summary>
    /// Mathematically proves the integrity of the IAM Flattening Engine under complex domain scenarios.
    /// </summary>
    public class ClaimFlatteningEngineTests
    {
        private readonly IClaimFlatteningEngine _engine;

        public ClaimFlatteningEngineTests()
        {
            // We use the actual in-memory store to prove our specific domain matrix works exactly as defined
            var store = new InMemoryAccessManagementStore();
            _engine = new ClaimFlatteningEngine(store);
        }

        [Fact]
        public void FlattenPermissions_ParentSubstitution_RemovesParentAndAddsChildren()
        {
            // Arrange
            // System Admin has Analytics.ExportAll and Analytics.ViewAll parent macros
            var roles = new[] { "System Administrator" };

            // Act
            var result = _engine.FlattenPermissions(roles, null).ToList();

            // Assert
            // The parent MUST be stripped out to save JWT space
            Assert.DoesNotContain(VantagePermissions.AnalyticsExportAll, result);
            Assert.DoesNotContain(VantagePermissions.AnalyticsViewAll, result);

            // The children MUST be injected
            Assert.Contains(VantagePermissions.AnalyticsExportExcel, result);
            Assert.Contains(VantagePermissions.AnalyticsExportCsv, result);
            Assert.Contains(VantagePermissions.AnalyticsExportPdf, result);
            Assert.Contains(VantagePermissions.AnalyticsViewAdvanced, result);
        }

        [Fact]
        public void FlattenPermissions_Deduplication_RemovesOverlappingPermissions()
        {
            // Arrange
            // Head Coach and Organization Manager share many overlapping permissions
            var roles = new[] { "Head Coach", "Organization Manager" };

            // Act
            var result = _engine.FlattenPermissions(roles, null).ToList();

            // Assert
            // Ensure no duplicate entries exist in the final flattened list
            Assert.Equal(result.Distinct().Count(), result.Count);

            // Ensure a shared parent macro unwrapped correctly
            Assert.DoesNotContain(VantagePermissions.RosterManageTransfer, result);
            Assert.Contains(VantagePermissions.RosterTransferRequest, result);
        }

        [Fact]
        public void FlattenPermissions_GhostRoles_IgnoresUnknownRolesSafely()
        {
            // Arrange
            var roles = new[] { "Player", "NonExistentGhostRole" };

            // Act
            var result = _engine.FlattenPermissions(roles, null).ToList();

            // Assert
            Assert.Contains(VantagePermissions.MatchmakingInHouseQueue, result);

            // The engine should not crash, and should only contain the 3 atomic Player permissions
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void FlattenPermissions_PremiumTierInjection_MergesBaseWithCustom()
        {
            // Arrange
            // Simulating a base player who bought an Enterprise-level tier or earned a custom reward
            var roles = new[] { "Free Agent Player" };
            var customPermissions = new[] { VantagePermissions.VisibilityUserL3, VantagePermissions.SystemBetaAccess };

            // Act
            var result = _engine.FlattenPermissions(roles, customPermissions).ToList();

            // Assert
            Assert.Contains(VantagePermissions.MatchmakingInHouseQueue, result); // From Base Role
            Assert.Contains(VantagePermissions.VisibilityUserL3, result); // From Custom Injection
            Assert.Contains(VantagePermissions.SystemBetaAccess, result); // From Custom Injection
        }
    }
}