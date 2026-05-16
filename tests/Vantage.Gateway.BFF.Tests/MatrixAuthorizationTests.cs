using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Vantage.Gateway.BFF.Tests
{
    /// <summary>
    /// Contains automated integration tests verifying the enforcement of the 3D Matrix Role-Based Access Control system.
    /// </summary>
    public class MatrixAuthorizationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixAuthorizationTests"/> class.
        /// </summary>
        /// <param name="factory">The web application factory used to generate an in-memory test server.</param>
        public MatrixAuthorizationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Helper method to acquire an authenticated session cookie from a simulated login endpoint.
        /// </summary>
        private async Task<string> GetAuthCookieAsync(HttpClient client, string loginEndpoint)
        {
            var content = new StringContent(string.Empty);
            var response = await client.PostAsync(loginEndpoint, content);
            response.EnsureSuccessStatusCode();

            var setCookieHeader = response.Headers.GetValues("Set-Cookie").First();
            // Extract just the cookie name and value to pass in subsequent requests
            return setCookieHeader.Split(';').First();
        }

        /// <summary>
        /// Verifies that an Organization Manager successfully accesses Organization-scoped endpoints, 
        /// but is strictly forbidden (403) from accessing Free Agent endpoints due to the tenant matrix.
        /// </summary>
        [Fact]
        public async Task Manager_WithOrgContext_CanAccessRoster_ButForbiddenFromEscrow()
        {
            var client = _factory.CreateClient();
            var cookie = await GetAuthCookieAsync(client, "/api/auth/login/manager");

            // 1. Test Valid Path (Org Context + Roster Manage Permission)
            var rosterRequest = new HttpRequestMessage(HttpMethod.Get, "/api/auth/test/roster");
            rosterRequest.Headers.Add("Cookie", cookie);

            var rosterResponse = await client.SendAsync(rosterRequest);
            Assert.Equal(HttpStatusCode.OK, rosterResponse.StatusCode);

            // 2. Test Invalid Path (Has Org Context, but lacks Escrow Permission)
            var escrowRequest = new HttpRequestMessage(HttpMethod.Get, "/api/auth/test/escrow");
            escrowRequest.Headers.Add("Cookie", cookie);

            var escrowResponse = await client.SendAsync(escrowRequest);
            Assert.Equal(HttpStatusCode.Forbidden, escrowResponse.StatusCode);
        }

        /// <summary>
        /// Verifies that a Free Agent Staff member successfully accesses Free Agent-scoped endpoints, 
        /// but is strictly forbidden (403) from accessing Organization endpoints due to missing context and permissions.
        /// </summary>
        [Fact]
        public async Task FreeAgent_WithoutOrgContext_CanAccessEscrow_ButForbiddenFromRoster()
        {
            var client = _factory.CreateClient();
            var cookie = await GetAuthCookieAsync(client, "/api/auth/login/freeagent");

            // 1. Test Valid Path (No Org Context + Escrow Permission)
            var escrowRequest = new HttpRequestMessage(HttpMethod.Get, "/api/auth/test/escrow");
            escrowRequest.Headers.Add("Cookie", cookie);

            var escrowResponse = await client.SendAsync(escrowRequest);
            Assert.Equal(HttpStatusCode.OK, escrowResponse.StatusCode);

            // 2. Test Invalid Path (Missing Org Context AND missing Roster Manage permission)
            var rosterRequest = new HttpRequestMessage(HttpMethod.Get, "/api/auth/test/roster");
            rosterRequest.Headers.Add("Cookie", cookie);

            var rosterResponse = await client.SendAsync(rosterRequest);
            Assert.Equal(HttpStatusCode.Forbidden, rosterResponse.StatusCode);
        }
    }
}