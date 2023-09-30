﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models.Fridge;

namespace Database.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Fridge.Fridge Fridge { get; set; }
    }
}
