using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public Fridge Fridge { get; set; }

    public List<Giveaway> GiveawaysAuthor { get; set; }

    public List<Giveaway> GiveawaysReceiver { get; set; }
}

