var builder = DistributedApplication.CreateBuilder(args);

var vehicleInfoApiService = builder.AddProject<Projects.VehicleInfo_ApiService>("vehicleInfoApiService")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.Insurance_ApiService>("insuranceApiService")
    .WithHttpHealthCheck("/health")
    .WithReference(vehicleInfoApiService);

builder.Build().Run();
