using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models { 
    // An entity class that describes the traits of our pizzas
    public class Pizza {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    // A context class that allows us to manipulate our local list of pizzas
    class PizzaDb : DbContext
    {
        public PizzaDb(DbContextOptions options) : base(options) { }
        public DbSet<Pizza> Pizzas { get; set; } = null!;
    }
}