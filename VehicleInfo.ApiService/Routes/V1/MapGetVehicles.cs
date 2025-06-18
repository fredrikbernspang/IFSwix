using Asp.Versioning;

public static class MapGetVehicle
{

    public static WebApplication MapGetVehicleRoute(this WebApplication app)
    {
        // Redirect root to Swagger UI
        app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
            .WithMetadata(new ExcludeFromDescriptionAttribute());

        var versionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .ReportApiVersions()
                .Build();

        var vehicleGroupV1 = app.MapGroup("/api/v{version:apiVersion}/vehicle")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0);

        vehicleGroupV1.MapGet("/", () =>
        {
            var resultset = GetVehicle.All();
            return resultset is not null
                ? Results.Ok(resultset)
                : Results.NotFound();
        })
        .WithName("GetAllVehicles")
        .WithOpenApi(operation =>
        {
            operation.Description = "Returns all vehicles found (testing purposes only).";
            return operation;
        });

        vehicleGroupV1.MapGet("/{regnum}", (string regnum) =>
        {
            var resultset = GetVehicle.Single(regnum);
            return resultset is not null
                ? Results.Ok(resultset)
                : Results.NotFound();
        })
        .WithName("GetVehicle")
        .WithOpenApi(operation =>
        {
            operation.Description = "Accepts a vehicle registration number and returns information about the vehicle.";
            return operation;
        });

        return app;
    }
}