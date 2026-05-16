var builder = DistributedApplication.CreateBuilder(args);

// Register the Identity Provider Microservice
var identityApi = builder.AddProject<Projects.Vantage_Identity_API>("identity-api");

// Register the BFF Gateway and pass the Identity API reference to it
// (The BFF will eventually need to talk to the IdP to validate credentials)
var bff = builder.AddProject<Projects.Vantage_Gateway_BFF>("bff")
                 .WithReference(identityApi);

builder.Build().Run();
