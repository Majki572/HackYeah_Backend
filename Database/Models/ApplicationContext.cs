﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models;
public class ApplicationContext : DbContext
{
    //private readonly IConfiguration _configuration;
    ////public FridgeContext(DbContextOptions<FridgeContext> options) : base(options) { }
    //public ApplicationContext(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer("Server=tcp:podzielsielodowkaserwer.database.windows.net,1433;Initial Catalog=podzielsielodowkabaza;Persist Security Info=False;User ID=adminhackyeah;Password=podzielsielodowka!2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");


    public DbSet<Fridge> Fridges { get; set; }
    public DbSet<ProductFridge> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Giveaway> Giveaways { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ProductDictionary> ProductDictionary { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Fridge>()
            .HasMany(e => e.Products)
            .WithOne(e => e.Fridge)
            .HasForeignKey(e => e.FridgeId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Giveaway>()
            .HasOne(e => e.Author)
            .WithMany(e => e.GiveawaysAuthor)
            .HasForeignKey(e => e.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Giveaway>()
            .HasOne(e => e.Receiver)
            .WithMany(e => e.GiveawaysReceiver)
            .HasForeignKey(e => e.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Giveaway>()
            .HasOne(e => e.Product)
            .WithMany(p => p.Giveaways)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<Conversation>()
            .HasMany(e => e.Messages)
            .WithOne(e => e.Conversation)
            .HasForeignKey(e => e.ConversationId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Message>()
            .HasOne(e => e.Conversation)
            .WithMany(e => e.Messages)
            .HasForeignKey(e => e.ConversationId);
    }

}

