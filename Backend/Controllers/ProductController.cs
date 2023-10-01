using AutoMapper;
using Backend.DTO;
using Database.Models;
using Database.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    public readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(
        IProductService fridgeService,
        IMapper mapper)
    {
        _productService = fridgeService;
        _mapper = mapper;
    }

    [HttpPost("AddProductToFridge")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductFridge))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Fridge>> AddProductToFridge([FromBody] ProductFridge product, int userId, int fridgeId)
    {
        var result = await _productService.AddProductToFridge(product, userId, fridgeId);

        if (result.Error == null)
        {
            return CreatedAtAction("GetProductById", new { id = fridgeId }, product);
        }
        return BadRequest(result.Error.Message);
    }

    [HttpGet("GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFridge))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDTO>>> GetProductById(int productId)
    {
        var result = await _productService.GetProductById(productId);

        if (result.Error == null)
        {
            return Ok(_mapper.Map<ProductDTO>(result.Product));
        }
        return BadRequest(result.Error.Message);
    }

    [HttpGet("GetProductsFromFridgeById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductFridge>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDTO>>> GetProductsFromFridgeById(int fridgeId)
    {
        var result = await _productService.GetProductsFromFridgeById(fridgeId);

        if (result.Error == null)
        {
            var dtoList = new List<ProductDTO>();
            foreach(var product in result.Products)
            {
                dtoList.Add(_mapper.Map<ProductDTO>(product));
            }
            return Ok(dtoList);
        }
        return BadRequest(result.Error.Message);
    }

    [HttpGet("GetProductsDictionary")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDictionary>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDictionary>>> GetProductsDictionary()
    {
        var result = await _productService.GetProductsDictionary();

        if (result.Error == null)
        {
            return Ok(result.ProductDictionary);
        }
        return BadRequest(result.Error.Message);
    }

    [HttpGet("UpdateProduct")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFridge))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDTO>>> UpdateProduct([FromBody] ProductFridge product, int userId, int fridgeId, int productId)
    {
        var result = await _productService.UpdateProduct(userId, fridgeId, productId, product);

        if (result.Error == null)
        {
            return CreatedAtAction("GetProductById", new { id = fridgeId }, product);
        }
        return BadRequest(result.Error.Message);
    }
}
