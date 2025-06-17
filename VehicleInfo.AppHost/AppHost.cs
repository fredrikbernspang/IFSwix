var builder = DistributedApplication.CreateBuilder(args);

var vehicleInfoApiservice = builder.AddProject<Projects.VehicleInfo_ApiService>("vehicleInfoApiService")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.Insurance_ApiService>("insuranceApiService")
    .WithHttpHealthCheck("/health")
    .WithReference(vehicleInfoApiservice)
    .WaitFor(vehicleInfoApiservice);

builder.Build().Run();
