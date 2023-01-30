using Microsoft.OpenApi.Models;
using PizzaStore.DB;

// Make a builder
var builder = WebApplication.CreateBuilder(args);

// Apply the swagger service to the builder
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzaStore API", Description = "Making the Pizzas you love", Version = "v1" });
});

// Build an app with the builder
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});

// Map hello world to the empty request
app.MapGet("/", () => "Hello World!");

// Mapping CRUD operations to DB operations
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
app.MapGet("/pizzas", () => { return PizzaDB.GetPizzas(); });
app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
app.MapPut("/pizzas", (Pizza pizza) => PizzaDB.UpdatePizza(pizza));
app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));

// Start the api and listen for requests from the client
app.Run();
