using Database.Models.Fridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Giveaway
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime CreateDate { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int ReceiverId { get; set; }

        public User? Receiver { get; set; }

        public int? Amount { get; set; }

        public string Description { get; set; } = null!;


    }
}
