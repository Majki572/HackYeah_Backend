using Database.Models;

namespace Backend.DTO
{
    public class GiveawayDTO
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } // Add a property for author's name
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProductName { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }

        // Constructor to initialize the DTO from a Giveaway entity
        public GiveawayDTO(Giveaway giveaway)
        {
            Id = giveaway.Id;
            AuthorName = giveaway.Author.Username; // Assuming Author has a Name property
            ExpirationDate = giveaway.ExpirationDate;
            CreateDate = giveaway.CreateDate;
            ProductName = giveaway.Product.Name;
            Amount = giveaway.Amount;
            Description = giveaway.Description;
        }
    }

}
