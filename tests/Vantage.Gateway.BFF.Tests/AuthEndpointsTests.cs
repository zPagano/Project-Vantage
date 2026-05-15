using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Vantage.Gateway.BFF.Tests
{
    /// <summary>
    /// Integration tests to verify the authentication endpoints and security boundaries of the Gateway.
    /// </summary>
    public class AuthEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthEndpointsTests"/> class.
        /// </summary>
        /// <param name="factory">The web application factory used to generate an in-memory test server.</param>
        public AuthEndpointsTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        /// <summary>
        /// Verifies that a successful login request generates the required HttpOnly, SameSite=Strict cookie.
        /// </summary>
        [Fact]
        public async Task Login_ReturnsSuccess_AndSetsSecureHttpOnlyCookie()
        {
            var content = new StringContent(string.Empty);

            var response = await _client.PostAsync("/api/auth/login", content);

            response.EnsureSuccessStatusCode();

            var setCookieHeaders = response.Headers.GetValues("Set-Cookie").ToList();

            Assert.NotEmpty(setCookieHeaders);

            var sessionCookie = setCookieHeaders.First(h => h.StartsWith("__Host-Vantage-Session"));

            Assert.Contains("httponly", sessionCookie.ToLower());
            Assert.Contains("samesite=strict", sessionCookie.ToLower());
        }
    }
}