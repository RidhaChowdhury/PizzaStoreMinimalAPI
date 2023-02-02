using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.DB;
using PizzaStore.Models;

// Make a builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PizzaDb>(options => options.UseInMemoryDatabase("items"));

// Apply the swagger service to the builder
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

#region Mapping CRUD operations to DB operations
app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));

// Get all pizzas from tha database
app.MapGet("/pizzas", async(PizzaDb dataBase) => await dataBase.Pizzas.ToListAsync());

// Adding a new pizza to the database
app.MapPost("/pizzas", async (PizzaDb dataBase, Pizza pizza) => {
    await dataBase.Pizzas.AddAsync(pizza);
    await dataBase.SaveChangesAsync();
    return Results.Created($"/pizza/{pizza.Id}", pizza);
});

// Updating a pizza in the database
app.MapPut("/pizza/{id}", async (PizzaDb db, Pizza updatepizza, int id) => {
    // Get the old pizza
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) return Results.NotFound();
    
    // Update it
    pizza.Name = updatepizza.Name;
    pizza.Description = updatepizza.Description;
    
    // Save the changes to the db
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) => {
    var pizza = await db.Pizzas.FindAsync(id);
    if (pizza is null) {
        return Results.NotFound();
    }
    db.Pizzas.Remove(pizza);
    await db.SaveChangesAsync();
    return Results.Ok();
});
# endregion

// Start the api and listen for requests from the client
app.Run();
