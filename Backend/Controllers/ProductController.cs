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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Fridge>> AddProductToFridge([FromBody] ProductDTOB product, int userId, int fridgeId)
    {
        var result = await _productService.AddProductToFridge(product, userId, fridgeId);

        if (result.ErrorMessage == null)
        {
            return CreatedAtAction("GetProductById", new { id = fridgeId }, product);
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("GetProductById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDTO>>> GetProductById(int productId)
    {
        var result = await _productService.GetProductById(productId);

        if (result.ErrorMessage == null)
        {
            return Ok(_mapper.Map<ProductDTO>(result.Product));
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("GetProductsFromFridgeById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDTO>>> GetProductsFromFridgeById(int fridgeId)
    {
        var result = await _productService.GetProductsFromFridgeById(fridgeId);

        if (result.ErrorMessage == null)
        {
            var dtoList = new List<ProductDTO>();
            foreach(var product in result.Products)
            {
                dtoList.Add(_mapper.Map<ProductDTO>(product));
            }
            return Ok(dtoList);
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpGet("GetProductsDictionary")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDictionary>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDictionaryDTO>>> GetProductsDictionary()
    {
        var result = await _productService.GetProductsDictionary();

        if (result.ErrorMessage == null)
        {
            var dtoList = new List<ProductDictionaryDTO>();
            foreach (var product in result.ProductDictionary)
            {
                dtoList.Add(_mapper.Map<ProductDictionaryDTO>(product));
            }
            return Ok(dtoList);
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("CreateProductsDictionary")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDictionary))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDictionaryDTO>>> CreateProductDictionary([FromBody] ProductDictionaryDTO product)
    {
        var result = await _productService.CreateProductsDictionary(_mapper.Map<ProductDictionary>(product));

        if (result.ErrorMessage == null)
        {
            return Ok();
        }
        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("UpdateProduct")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductFridge))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ProductDTO>>> UpdateProduct([FromBody] ProductDTOB product, int productId)
    {
        var result = await _productService.UpdateProduct(product, productId);

        if (result.ErrorMessage == null)
        {
            return CreatedAtAction("GetProductById", new { id = productId }, product);
        }
        return BadRequest(result.ErrorMessage);
    }
}
