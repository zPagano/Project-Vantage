var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Vantage_Gateway_BFF>("bff");

builder.Build().Run();
