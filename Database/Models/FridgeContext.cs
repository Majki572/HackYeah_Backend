using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models;
public class FridgeContext : DbContext
{
    //private readonly IConfiguration _configuration;
    //public FridgeContext(DbContextOptions<FridgeContext> options) : base(options) { }
    //public FridgeContext(IConfiguration configuration) 
    //{
    //    _configuration = configuration;
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlite("Data Source=../Database/Database/DBsql.db");


    public DbSet<Fridge> Fridges { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

}

