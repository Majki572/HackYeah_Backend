using Database.Models;

namespace Backend.DTO
{
    public class GiveawayDTO
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } // Add a property for author's name
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProductName { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }


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
            Longitude = giveaway.Longitude;
            Latitude = giveaway.Latitude;
        }

        // Cast giveawayDTO to Giveaway
        public static explicit operator Giveaway(GiveawayDTO giveawayDTO)
        {
            return new Giveaway
            {
                ExpirationDate = giveawayDTO.ExpirationDate,
                CreateDate = giveawayDTO.CreateDate,
                Amount = giveawayDTO.Amount,
                Description = giveawayDTO.Description,
                AuthorId = giveawayDTO.AuthorId,
                Latitude = giveawayDTO.Latitude,
                Longitude = giveawayDTO.Longitude,
            };
        }   
    }

}
