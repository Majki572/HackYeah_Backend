using Database.Models;
using Database.Services.Interfaces;
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

    public async Task<BackendResponse> CreateGiveaway(Giveaway giveaway, int userID)
    {
        BackendResponse backendResponse = new BackendResponse();
        try
        {
            await _context.Giveaways.AddAsync(giveaway);
        }
        catch(Exception ex)
        {
            backendResponse = new BackendResponse();
            backendResponse.Error.Message = ex.Message;
        }
        return backendResponse;
    }

    public async Task<BackendResponse> GetGiveaway(int giveawayId)
    {
        BackendResponse backendResponse = new BackendResponse();
        if (giveawayId <= 0)
        {
            string errorMessage = "Id must be greater than zero.";
            backendResponse.Error.Message = errorMessage;
            return backendResponse;
        }

        var result = await _context.Giveaways.FindAsync(giveawayId);
        backendResponse.Giveaway = result;

        return backendResponse;

    }
}
