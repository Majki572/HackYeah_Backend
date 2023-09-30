using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Database.Models;
using System.Drawing.Drawing2D;
using Database.Services;
using Backend.DTO;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiveawaysController : ControllerBase
    {
        private readonly ApplicationContext _context;

        private readonly GiveawayService giveawayService;

        public GiveawaysController(ApplicationContext context)
        {
            _context = context;
            giveawayService = new GiveawayService(context);
        }

        // GET: api/Giveaways
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Giveaway>>> GetGiveaways([FromQuery]Coordinates? coordinates, double? maxDistance)
        {
            if (coordinates != null && maxDistance != null)
            {
                var result = await giveawayService.GetGiveaways(coordinates, (double)maxDistance);

                if (result.Error.Message == null)
                {
                    return Ok(result.Giveaways);
                }
                else
                {
                    return BadRequest(result.Error);
                }
            }
            else
            {
                var result = await giveawayService.GetGiveaways();

                if(result.Error.Message == null)
                {
                    return Ok(result.Giveaways);
                }
                else
                {
                    return BadRequest(result.Error);
                }
            }
        }

        // GET: api/Giveaways/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Giveaway>> GetGiveaway(int id)
        {
            if (_context.Giveaways == null)
            {
                return NotFound();
            }
            var giveaway = await _context.Giveaways.FindAsync(id);

            if (giveaway == null)
            {
                return NotFound();
            }

            return giveaway;
        }

        // PUT: api/Giveaways/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiveaway(int id, Giveaway giveaway)
        {
            if (id != giveaway.Id)
            {
                return BadRequest();
            }

            _context.Entry(giveaway).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiveawayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Giveaways
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Giveaway>> PostGiveaway([FromBody]NewGiveawayDTO giveaway)
        {
            var result = await giveawayService.CreateGiveaway((Giveaway)giveaway);

            if(result.Error.Message == null)
            {
                return Ok(result.Giveaway);
            }
            else
            {
                return BadRequest(result.Error.Message);
            }   
        }

        // DELETE: api/Giveaways/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiveaway(int id)
        {
            if (_context.Giveaways == null)
            {
                return NotFound();
            }
            var giveaway = await _context.Giveaways.FindAsync(id);
            if (giveaway == null)
            {
                return NotFound();
            }

            _context.Giveaways.Remove(giveaway);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GiveawayExists(int id)
        {
            return (_context.Giveaways?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
