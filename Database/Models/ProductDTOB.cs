using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models;

public class ProductDTOB
{
    public int ProductDictionaryId { get; set; }
    public int Quantity { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Description { get; set; }
    public int FridgeId { get; set; }

    public int? Weight { get; set; }
    public int? Calories { get; set; }
}
