using Vantage.Presentation.Hosting.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire OpenTelemetry and Resilience Defaults
builder.AddServiceDefaults();

// Add our custom RFC 9457 Error Handling
builder.Services.AddVantageErrorHandling();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapDefaultEndpoints(); // Aspire health checks
app.UseVantageErrorHandling();

app.MapGet("/", () => "Vantage BFF Gateway is running.");

app.Run();