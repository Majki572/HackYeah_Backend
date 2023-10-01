using Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MealController : ControllerBase
{
    public bool isMeal = true;

    [HttpPost]
    public async Task<ActionResult<Giveaway>> CreateMealGiveway([FromBody] Giveaway giveway)
    {

    }
}
