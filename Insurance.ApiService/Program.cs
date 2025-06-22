using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults (telemetry, health checks, etc.)
builder.AddServiceDefaults();

// Add services
builder.Services.AddProblemDetails();

// Add versioning services
builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// Add http client with Aspire service discovery
builder.Services.AddHttpClient("vehicleInfoApiService", client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        client.BaseAddress = new("https+http://vehicleInfoApiService");
    });

// Add OpenAPI/Swagger for development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

// Map Aspire default endpoints (health checks, etc.)
app.MapDefaultEndpoints();

// Add endpoints
app.MapGetPersonRoute();

app.Run();