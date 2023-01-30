using Microsoft.OpenApi.Models;

// Make a builder
var builder = WebApplication.CreateBuilder(args);

// Apply the swagger service to the builder
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizza API", Description = "Grab a slice", Version = "v1" });
});

// Build an app with the builder
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
});

// Map hello world to the empty request
app.MapGet("/", () => "Hello World!");

// Start the api and listen for requests from the client
app.Run();
