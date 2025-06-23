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

        var personGroupV1 = app.MapGroup("/api/v{version:apiVersion}/person")
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(1.0);

        personGroupV1.MapGet("/{id}", async (
            IHttpClientFactory httpClientFactory,
            string id,
            bool mockVehicleApi = false // Query parameter
        ) =>
        {
            var resultset = await GetPerson.SingleAsync(httpClientFactory, id, mockVehicleApi);
            if (resultset is null)
                return Results.NotFound();

            // Convert the resultset to JSON with Newtonsoft.Json for polymorphic serialization (CarInsurance <> Insurance)
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(resultset);
            return Results.Content(json, "application/json");
        })
        .WithName("GetPersonById")
        .Produces<InsuranceInfo.Models.Person>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation =>
        {
            operation.Description = "Accepts a personal identification number and returns all the insurances the person has, along with their monthly costs.";
            return operation;
        });

                // Only expose this endpoint in dev
        if (app.Environment.IsDevelopment())
        {
            personGroupV1.MapGet("/", () =>
            {
                var resultset = GetPerson.All();
                if (resultset is null)
                    return Results.NotFound();

                // Convert the resultset to JSON with Newtonsoft.Json for polymorphic serialization (CarInsurance <> Insurance)
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(resultset);
                return Results.Content(json, "application/json");
            })
            .WithName("GetAllPersons")
            .WithOpenApi(operation =>
            {
                operation.Description = "Returns all persons found and their insurances (testing purposes only).";
                return operation;
            });
        }

        return app;
    }
}