namespace PizzaStore.DB;

// A prototype for what a pizza looks like

public record OldPizza
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

// The actual database of pizza's
public class PizzaDB
{
    public static string test() {
        return "test";
    }
    // The pizzas are stored in a list
    private static List<OldPizza> _pizzas = new List<OldPizza>()
    {
     new OldPizza{ Id=1, Name="Montemagno, Pizza shaped like a great mountain" },
     new OldPizza{ Id=2, Name="The Galloway, Pizza shaped like a submarine, silent but deadly"},
     new OldPizza{ Id=3, Name="The Noring, Pizza shaped like a Viking helmet, where's the mead"}
   };

    // Returns the list of pizzas
    public static List<OldPizza> GetPizzas() {
        System.Diagnostics.Debug.WriteLine(_pizzas.Count);
        return _pizzas;
    }

    // Get a specific pizza
    public static OldPizza? GetPizza(int id) {
        return _pizzas.SingleOrDefault(pizza => pizza.Id == id);
    }

    // Add a new pizza to the db
    public static OldPizza CreatePizza(OldPizza pizza) {
        _pizzas.Add(pizza);
        return pizza;
    }

    // Update a specific pizza
    public static OldPizza UpdatePizza(OldPizza update) {
        _pizzas = _pizzas.Select(pizza =>
        {
            if (pizza.Id == update.Id) {
                pizza.Name = update.Name;
            }
            return pizza;
        }).ToList();
        return update;
    }

    // Remove a pizza specified by ID
    public static void RemovePizza(int id) {
        _pizzas = _pizzas.FindAll(pizza => pizza.Id != id).ToList();
    }
}