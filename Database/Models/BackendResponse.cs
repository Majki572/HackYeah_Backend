﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models;

public class BackendResponse
{
    public Fridge Fridge {  get; set; }
    public User User { get; set; }
    public ProductFridge Product { get; set; }
    public List<ProductFridge> Products { get; set; }
    public string ErrorMessage { get; set; }
    public List<Giveaway> Giveaways { get; set;}
    public List<ProductDictionary> ProductDictionary { get; set; }

    public Giveaway Giveaway { get; set; }
}
