using Vantage.Gateway.BFF.Endpoints;
using Vantage.Presentation.Hosting.Errors;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddVantageErrorHandling();

// Add Authentication and Authorization
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "__Host-Vantage-Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Requires HTTPS

    // For an API/BFF, we do not want to redirect to a login page on failure.
    // We simply return a 401 Unauthorized status code to the SPA client.
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseVantageErrorHandling();

// Add Auth Middleware to the pipeline
app.UseAuthentication();
app.UseAuthorization();

// Map our Authentication Endpoints
app.MapAuthEndpoints();

app.Run();

public partial class Program { }