using Database.Models;
using Database.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services;

public class GiveawayService
{
    public readonly ApplicationContext _context;

    public GiveawayService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<BackendResponse> ClaimGiveaway(int giveawayId, int claimerId)
    {
        BackendResponse backendResponse = new BackendResponse();

        var giveawayFromDb = await _context.Giveaways.FindAsync(giveawayId);
        if (giveawayFromDb == null)
        {
            backendResponse.ErrorMessage = "Giveaway not found";
            return backendResponse;
        }

        var claimer = await _context.Users.FindAsync(claimerId);
        if (claimer == null)
        {
            backendResponse.ErrorMessage = "User not found";
            return backendResponse;
        }

        giveawayFromDb.Receiver = claimer;

        try
        {
            _context.Giveaways.Update(giveawayFromDb);
            await _context.SaveChangesAsync();
            backendResponse.Giveaway = giveawayFromDb;
        }
        catch (Exception ex)
        {
            backendResponse.ErrorMessage = ex.Message;
        }

        Conversation conversation = new Conversation()
        {
            User1Id = giveawayFromDb.AuthorId,
            User2Id = claimerId
        };

        try
        {
            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            backendResponse.ErrorMessage = ex.Message;
        }

        return backendResponse;


    }

    public async Task<BackendResponse> CreateGiveaway(Giveaway giveaway)
    {
        BackendResponse backendResponse = new BackendResponse();
        var product = _context.ProductDictionary.Where(p => p.Name == giveaway.Product.Name).FirstOrDefault();
        var author = _context.Users.Where(u => u.Id == giveaway.AuthorId).FirstOrDefault();
        if (author == null)
        {
            backendResponse.ErrorMessage = "Author not found";
            return backendResponse;
        }
        giveaway.Author = author;

        if (product == null)
        {
            // create new product
            product = new ProductDictionary();
            product.Name = giveaway.Product.Name;
            try
            {
                await _context.ProductDictionary.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                backendResponse = new BackendResponse();
                backendResponse.ErrorMessage = ex.Message;
                return backendResponse;
            }
        }
        giveaway.Product = product;
        try
        {
            await _context.Giveaways.AddAsync(giveaway);
            await _context.SaveChangesAsync();
            backendResponse.Giveaway = giveaway;
        }
        catch (Exception ex)
        {
            backendResponse = new BackendResponse();
            backendResponse.ErrorMessage = ex.Message;
        }
        return backendResponse;
    }

    public async Task<BackendResponse> GetGiveaway(int giveawayId)
    {
        BackendResponse backendResponse = new BackendResponse();
        if (giveawayId <= 0)
        {
            string errorMessage = "Id must be greater than zero.";
            backendResponse.ErrorMessage = errorMessage;
            return backendResponse;
        }

        var result = await _context.Giveaways.FindAsync(giveawayId);

        _context.Entry(result).Reference(r => r.Author).Load();

        if (result == null)
        {
            backendResponse.ErrorMessage = "Not found";
        }
        backendResponse.Giveaway = result;

        return backendResponse;
    }

    public async Task<BackendResponse> GetGiveaways(Coordinates coordinates, double maxDistance)
    {
        BackendResponse backendResponse = new BackendResponse();

        try
        {
            var result = _context.Giveaways.Include(g => g.Author).Include(g => g.Product).ToList();

            backendResponse.Giveaways = result.Where(g => CalculateDistance(coordinates, new Coordinates(g.Latitude, g.Longitude)) < maxDistance).ToList();
        }
        catch (Exception e)
        {
            backendResponse.ErrorMessage = e.Message;
        }


        return backendResponse;
    }

    public async Task<BackendResponse> GetGiveaways()
    {
        BackendResponse backendResponse = new BackendResponse();

        try
        {
            var result = _context.Giveaways.Include(g => g.Author).Include(g => g.Product).ToList();
            backendResponse.Giveaways = result;
        }
        catch (Exception e)
        {
            backendResponse.ErrorMessage = e.Message;
        }


        return backendResponse;
    }

    // public async Task<BackendResponse>

    private static double CalculateDistance(Coordinates coord1, Coordinates coord2)
    {
        const double earthRadius = 6371;

        // Convert degrees to radians
        double lat1Rad = ToRadians(coord1.Latitude);
        double lon1Rad = ToRadians(coord1.Longitude);
        double lat2Rad = ToRadians(coord2.Latitude);
        double lon2Rad = ToRadians(coord2.Longitude);

        // Haversine formula
        double dLat = lat2Rad - lat1Rad;
        double dLon = lon2Rad - lon1Rad;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = earthRadius * c;

        return distance;
    }

    private static double ToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
}

public class Coordinates
{
    public Coordinates(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public Coordinates() { }

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}