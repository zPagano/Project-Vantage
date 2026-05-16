using Vantage.Presentation.Hosting.Errors;
using Vantage.Presentation.Hosting.Localization;
using Vantage.Identity.API.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Register Aspire Service Defaults
builder.AddServiceDefaults();

// Register Localization (Provides IStringLocalizer)
builder.Services.AddVantageLocalization();
// Register Error Handling (Consumes IStringLocalizer)
builder.Services.AddVantageErrorHandling();

// Register IAM services as Singletons/Transients for the identity pipeline
builder.Services.AddSingleton<IAccessManagementStore, InMemoryAccessManagementStore>();
builder.Services.AddTransient<IClaimFlatteningEngine, ClaimFlatteningEngine>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Use Localization Middleware first so exceptions are caught in the correct language
app.UseVantageRequestLocalization();
// Use Error Handling Middleware
app.UseVantageErrorHandling();

app.MapGet("/", () => "Vantage Identity Provider is running.");

#region Temporary Diagnostic Endpoints

app.MapPost("/api/diagnostics/flatten", (
    [FromBody] FlattenDiagnosticRequest request,
    IClaimFlatteningEngine engine) =>
{
    var flattened = engine.FlattenPermissions(request.Roles, request.CustomPermissions);

    return Results.Ok(new
    {
        OriginalRoles = request.Roles,
        OriginalCustomPermissions = request.CustomPermissions,
        FlattenedCount = flattened.Count(),
        FlattenedPermissions = flattened.OrderBy(p => p)
    });
});

#endregion

app.Run();

#region Diagnostic Models
public record FlattenDiagnosticRequest(string[] Roles, string[] CustomPermissions);
#endregion