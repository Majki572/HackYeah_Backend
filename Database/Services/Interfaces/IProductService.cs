using Database.Models;

namespace Database.Services.Interfaces;

public interface IProductService
{
    public Task<BackendResponse> AddProductToFridge(ProductFridge product, int userId, int fridgeId);
    public Task<BackendResponse> RemoveProductFromFridge(ProductFridge product, int userId, int fridgeId);
    public Task<BackendResponse> GetProductsFromFridge(int fridgeId);
}