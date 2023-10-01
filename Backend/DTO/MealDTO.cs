using Database.Models;

namespace Backend.DTO;

public class MealDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int AuthorId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreateDate { get; set; }
    public int? ReceiverId { get; set; }
    public DateTime? GetMealDate { get; set; }
    public DateTime? OfferMealDate { get; set; }
}
