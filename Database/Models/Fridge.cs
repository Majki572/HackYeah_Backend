namespace Database.Models;
public class Fridge
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<ProductFridge> Products { get; set; }
}

