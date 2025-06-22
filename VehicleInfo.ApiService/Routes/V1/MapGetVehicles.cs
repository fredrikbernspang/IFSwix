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

        vehicleGroupV1.MapGet("/{licensePlate}", (string licensePlate) =>
        {
            var resultset = GetVehicle.Single(licensePlate);
            return resultset is not null
                ? Results.Ok(resultset)
                : Results.NotFound();
        })
        .WithName("GetVehicle")
        .Produces<VehicleInfo.Models.Vehicle>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation =>
        {
            operation.Description = "Accepts a vehicle license plate and returns information about the vehicle.";
            return operation;
        });

        if (app.Environment.IsDevelopment())
        {
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
        }

        return app;
    }
}