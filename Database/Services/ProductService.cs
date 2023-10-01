using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Database.Models;
using Database.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Services;

public class ProductService : IProductService
{
    public ApplicationContext _context { get; set; }
    public readonly IMapper _mapper;
    public ProductService(
        ApplicationContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<BackendResponse> AddProductToFridge(ProductDTOB product, int userId, int fridgeId)
    {
        var response = new BackendResponse();
        var dictionaryProduct = await _context.ProductDictionary.FindAsync(product.ProductDictionaryId);
        if (dictionaryProduct == null)
        {
            response.ErrorMessage = "Product with that id does not exist, please add to dictionary first.";
            return response;
        }
        var productFridge = new ProductFridge();
        productFridge.Quantity = product.Quantity;
        productFridge.ExpirationDate = product.ExpirationDate;
        productFridge.Description = product.Description;
        productFridge.FridgeId = product.FridgeId;
        productFridge.Weight = product.Weight;
        productFridge.Calories = product.Calories;
        productFridge.Name = dictionaryProduct.Name;


        //if (userId != fridgeId)
        //{
        //    response.ErrorMessage = "User id and fridge id does not match.";
        //    return response;
        //}
        await _context.Products.AddAsync(productFridge);
        await _context.SaveChangesAsync();

        return response;
    }

    public async Task<BackendResponse> GetProductsFromFridgeById(int fridgeId)
    {
        var response = new BackendResponse();
        if (fridgeId <= 0)
        {
            response.ErrorMessage = "This fridge does not have any products.";
            return response;
        }

        response.Products = await _context.Products.Where(x => x.FridgeId == fridgeId).ToListAsync();

        if (!response.Products.Any())
        {
            response.ErrorMessage = "This fridge does not have any products.";
            return response;
        }

        return response;
    }

    public async Task<BackendResponse> RemoveProductFromFridge(ProductFridge product, int userId, int fridgeId)
    {
        var response = new BackendResponse();
        //if (userId != fridgeId)
        //{
        //    response.ErrorMessage = "User id and fridge id does not match.";
        //    return response;
        //}

        var dbProduct = await _context.Products.FindAsync(product.Id);

        if (dbProduct == null)
        {
            response.ErrorMessage = "Product does not exist in this fridge.";
            return response;
        }

        var result = _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return response;
    }

    public async Task<BackendResponse> GetProductById(int productId)
    {
        BackendResponse backendResponse = new BackendResponse();
        if (productId <= 0)
        {
            string errorMessage = "Product id must be greater than zero.";
            backendResponse.ErrorMessage = errorMessage;
            return backendResponse;
        }

        var result = await _context.Products.FindAsync(productId);

        backendResponse.Product = result;

        return backendResponse;
    }

    public async Task<BackendResponse> GetProductsDictionary()
    {
        BackendResponse backendResponse = new BackendResponse();

        var result = await _context.ProductDictionary.ToListAsync();

        backendResponse.ProductDictionary = result;

        return backendResponse;
    }

    public async Task<BackendResponse> UpdateProduct(ProductDTOB product, int productId)
    {
        var response = new BackendResponse();
        //if (userId != fridgeId)
        //{
        //    response.ErrorMessage = "User id and fridge id does not match.";
        //    return response;
        //}
        //if (product.Id != productId)
        //{
        //    response.ErrorMessage = "Provided product id and product id does not match.";
        //    return response;
        //}
        //if (productId <= 0 || fridgeId <= 0 || userId <= 0)
        //{
        //    string errorMessage = "Id must be greater than zero.";
        //    response.ErrorMessage = errorMessage;
        //    return response;
        //}

        var result = await _context.Products.FindAsync(productId);
        if (result == null)
        {
            response.ErrorMessage = "Product does not exist";
            return response;
        }
        result.Quantity = product.Quantity;
        result.ExpirationDate = product.ExpirationDate;
        result.Description = product.Description;
        result.Weight = product.Weight;
        result.Calories = product.Calories;
        _context.Products.Update(result);
        await _context.SaveChangesAsync();

        return response;
    }

    public async Task<BackendResponse> CreateProductsDictionary(ProductDictionary productDictionary)
    {
        var response = new BackendResponse();
        var result = await _context.ProductDictionary.AddAsync(productDictionary);
        await _context.SaveChangesAsync();
        return response;
    }
}
