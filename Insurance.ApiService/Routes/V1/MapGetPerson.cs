using Asp.Versioning;

public static class MapGetPerson
{

    public static WebApplication MapGetPersonRoute(this WebApplication app)
    {
        // Redirect root to Swagger UI
        app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
            .WithMetadata(new ExcludeFromDescriptionAttribute()); 

        var versionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .ReportApiVersions()
                .Build();

        var vehicleGroupV1 = app.MapGroup("/api/v{version:apiVersion}/person")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0);

        if (app.Environment.IsDevelopment())
        {
            vehicleGroupV1.MapGet("/", () =>
            {
                var resultset = GetPerson.All();
                return resultset is not null
                    ? Results.Ok(resultset)
                    : Results.NotFound();
            })
            .WithName("GetAllPersons")
            .WithOpenApi(operation =>
            {
                operation.Description = "Returns all persons found and their insurances (testing purposes only).";
                return operation;
            });
        }

        vehicleGroupV1.MapGet("/{id}", (string id) =>
        {
            var resultset = GetPerson.Single(id);
            return resultset is not null
                ? Results.Ok(resultset)
                : Results.NotFound();
        })
        .WithName("GetPersonById")
        .WithOpenApi(operation =>
        {
            operation.Description = "Accepts a personal identification number and returns all the insurances the person has, along with their monthly costs.";
            return operation;
        });

        return app;
    }
}