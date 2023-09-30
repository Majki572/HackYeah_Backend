using Database.Models;

namespace Database.Services.Interfaces;

public interface IProductService
{
    public Task<BackendResponse> AddProductToFridge(ProductFridge product, int userId, int fridgeId);
    public Task<BackendResponse> RemoveProductFromFridge(ProductFridge product, int userId, int fridgeId);
    public Task<BackendResponse> GetProductsFromFridgeById(int fridgeId);
    public Task<BackendResponse> GetProductById(int productId);
    public Task<BackendResponse> UpdateProduct(int userId, int fridgeId, int productId, ProductFridge product);
    public Task<BackendResponse> GetProductsDictionary();

}