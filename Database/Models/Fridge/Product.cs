namespace Database.Models.Fridge;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ExpirationDate { get; set; }
    public string Description { get; set; }
    public double? Weight { get; set; }
    public double? Calories { get; set; }

    public int FridgeId { get; set; }

    public Fridge Fridge { get; set; }
}
