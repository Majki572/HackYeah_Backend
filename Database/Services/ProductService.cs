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
    public Task<BackendResponse> AddProductToFridge(Product product, int userId, int fridgeId)
    {
        throw new NotImplementedException();

    }

    public Task<BackendResponse> GetProductsFromFridge(int fridgeId)
    {
        throw new NotImplementedException();
    }

    public Task<BackendResponse> RemoveProductFromFridge(Product product, int userId, int fridgeId)
    {
        throw new NotImplementedException();
    }
}
