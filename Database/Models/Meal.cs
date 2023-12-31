﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models;

public class Meal
{
    public int Id { get; set; }
    public string Description {  get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime CreateDate { get; set; }
    public int? ReceiverId { get; set; }
    public User? Receiver { get; set; }
    public DateTime? GetMealDate { get; set; }
    public DateTime? OfferMealDate { get; set; }
}
