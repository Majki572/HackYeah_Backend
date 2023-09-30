namespace Database.Models;
public class ProductFridge
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ExpirationDate { get; set; }
    public string Description { get; set; }
    public int FridgeId { get; set; }

    public Fridge Fridge { get; set; }
    public int? Weight { get; set; }
    public int? Calories { get; set; }
}
