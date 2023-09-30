namespace Database.Models.Fridge;
public class Fridge {
    public int Id { get; set; } 
    public string Name { get; set; }
    public List<Product> ProductList { get; set; }
}

