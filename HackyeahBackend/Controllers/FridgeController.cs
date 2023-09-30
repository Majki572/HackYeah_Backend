using Database.Models;
using Database.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FridgeController : ControllerBase
{
    public readonly IFridgeService _fridgeService;
    public FridgeController(IFridgeService fridgeService)
    {
        _fridgeService = fridgeService;
    }
    
    [HttpPost("CreateFridge")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Fridge))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Fridge>> CreateFridge([FromBody] Fridge fridge, int userId)
    {
        var result = await _fridgeService.CreateFridge(fridge, userId);

        if(result.Error != null)
        {
            return CreatedAtAction("GetFridgeById", new { id = fridge.Id }, fridge);
        }
        return BadRequest(result.Error.Message);
    }

    [HttpGet("GetFridgeById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Fridge))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Fridge>> GetFridgeById(int fridgeId)
    {
        var result = await _fridgeService.GetFridgeById(fridgeId);

        if (result.Error != null)
        {
            return Ok(result.Fridge);
        }
        return BadRequest(result.Error.Message);
    }
}
