using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Database.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Services;

public class ProductService : IProductService
{
    public ApplicationContext _context { get; set; }
    public ProductService(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<BackendResponse> AddProductToFridge(ProductFridge product, int userId, int fridgeId)
    {
        var response = new BackendResponse();
        var dbProduct = await _context.Products.FindAsync(product.Id);
        if (dbProduct != null)
        {
            response.Error.Message = "Product with that id already exists.";
            return response;
        }

        if(userId != fridgeId)
        {
            response.Error.Message = "User id and fridge id does not match.";
            return response;
        }
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return response;
    }

    public async Task<BackendResponse> GetProductsFromFridge(int fridgeId)
    {
        var response = new BackendResponse();
        if (fridgeId <= 0)
        {
            response.Error.Message = "This fridge does not have any products.";
            return response;
        }

        response.Products = await _context.Products.Where(x => x.FridgeId == fridgeId).ToListAsync();

        if (!response.Products.Any())
        {
            response.Error.Message = "This fridge does not have any products.";
            return response;
        }

        return response;
    }

    public async Task<BackendResponse> RemoveProductFromFridge(ProductFridge product, int userId, int fridgeId)
    {
        var response = new BackendResponse();
        if (userId != fridgeId)
        {
            response.Error.Message = "User id and fridge id does not match.";
            return response;
        }

        var dbProduct = await _context.Products.FindAsync(product.Id);

        if (dbProduct == null)
        {
            response.Error.Message = "Product does not exist in this fridge.";
            return response;
        }

        var result = _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return response;
    }
}
