using Microsoft.OpenApi.Models;

// Make a builder and build an app with it
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizza API", Description = "Grab a slice", Version = "v1" });
});

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
