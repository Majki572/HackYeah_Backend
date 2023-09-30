using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Interfaces;

public interface IFridgeService
{
    public Task<BackendResponse> GetFridgeById(int fridgeId);
    public Task<BackendResponse> CreateFridge(Fridge fridge, int userId);
}
