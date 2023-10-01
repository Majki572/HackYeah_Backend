using Database.Models;

namespace Database.Services.Interfaces;

public interface IProductService
{
    public Task<BackendResponse> AddProductToFridge(ProductDTOB product, int userId, int fridgeId);
    public Task<BackendResponse> RemoveProductFromFridge(ProductFridge product, int userId, int fridgeId);
    public Task<BackendResponse> GetProductsFromFridgeById(int fridgeId);
    public Task<BackendResponse> GetProductById(int productId);
    public Task<BackendResponse> UpdateProduct(ProductDTOB product, int productId);
    public Task<BackendResponse> GetProductsDictionary();
    public Task<BackendResponse> CreateProductsDictionary(ProductDictionary productDictionary);

}