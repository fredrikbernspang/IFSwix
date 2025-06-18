using Asp.Versioning;

public static class MapGetInsurance
{

    public static WebApplication MapGetInsuranceRoute(this WebApplication app)
    {
        // Redirect root to Swagger UI
        app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
            .WithMetadata(new ExcludeFromDescriptionAttribute()); 

        var versionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .ReportApiVersions()
                .Build();

        var vehicleGroupV1 = app.MapGroup("/api/v{version:apiVersion}/insurance")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0);

        vehicleGroupV1.MapGet("/", () =>
        {
            var resultset = GetInsurance.All();
            return resultset is not null
                ? Results.Ok(resultset)
                : Results.NotFound();
        })
        .WithName("GetAllInsurances")
        .WithOpenApi(operation =>
        {
            operation.Description = "Returns all insurances found (testing purposes only).";
            return operation;
        });

        vehicleGroupV1.MapGet("/{id}", (string id) =>
        {
            var resultset = GetInsurance.Single(id);
            return resultset is not null
                ? Results.Ok(resultset)
                : Results.NotFound();
        })
        .WithName("GetInsurance")
        .WithOpenApi(operation =>
        {
            operation.Description = "Accepts a personal identification number and returns all the insurances the person has, along with their monthly costs.";
            return operation;
        });

        return app;
    }
}