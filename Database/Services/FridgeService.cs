using Database.Models;
using Database.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services;

public class FridgeService : IFridgeService
{
    public readonly ApplicationContext _context;
    
    public FridgeService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<BackendResponse> CreateFridge(Fridge fridge, int userID)
    {
        BackendResponse backendResponse = new BackendResponse();
        if(fridge == null || userID <= 0)
        {
            string errorMessage = "Fridge must be not null and user id must be greater than zero.";
            backendResponse.Error.Message = errorMessage;
            return backendResponse;
        }

        await _context.Fridges.AddAsync(fridge);
        await _context.SaveChangesAsync();

        return backendResponse;
    }

    public async Task<BackendResponse> GetFridgeById(int fridgeId)
    {
        BackendResponse backendResponse = new BackendResponse();
        if (fridgeId <= 0)
        {
            string errorMessage = "Fridge id must be greater than zero.";
            backendResponse.Error.Message = errorMessage;
            return backendResponse;
        }

        var result = await _context.Fridges.FindAsync(fridgeId);
        await _context.SaveChangesAsync();

        backendResponse.Fridge = result;

        return backendResponse;

    }
}
