using Database.Models;

namespace Backend.DTO
{
    public class NewGiveawayDTO
    {
        public int AuthorId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ProductName { get; set; }
        public int? Amount { get; set; }
        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public NewGiveawayDTO() { }

        // Constructor to initialize the DTO from a Giveaway entity
        public NewGiveawayDTO(Giveaway giveaway)
        {
            ExpirationDate = giveaway.ExpirationDate;
            ProductName = giveaway.Product.Name;
            Amount = giveaway.Amount;
            Description = giveaway.Description;
            AuthorId = giveaway.AuthorId;
            Latitude = giveaway.Latitude;
            Longitude = giveaway.Longitude;
        }

        // Cast giveawayDTO to Giveaway
        public static explicit operator Giveaway(NewGiveawayDTO giveawayDTO)
        {
            return new Giveaway
            {
                ExpirationDate = giveawayDTO.ExpirationDate,
                CreateDate = DateTime.Now,
                Amount = giveawayDTO.Amount,
                Description = giveawayDTO.Description,
                AuthorId = giveawayDTO.AuthorId,
                Latitude = giveawayDTO.Latitude,
                Longitude = giveawayDTO.Longitude,
                Product = new ProductDictionary
                {
                    Name = giveawayDTO.ProductName
                }
            };
        }   
    }

}
