using Database.Models;

namespace Database.Services.Interfaces;

public interface IProductService
{
    public Task<BackendResponse> AddProductToFridge(Product product, int userId, int fridgeId);
    public Task<BackendResponse> RemoveProductFromFridge(Product product, int userId, int fridgeId);
    public Task<BackendResponse> GetProductsFromFridge(int fridgeId);
}