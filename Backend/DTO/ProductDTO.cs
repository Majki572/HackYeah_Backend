using Database.Models;

namespace Backend.DTO;

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Description { get; set; }
    public int FridgeId { get; set; }
    public int? Weight { get; set; }
    public int? Calories { get; set; }
}
