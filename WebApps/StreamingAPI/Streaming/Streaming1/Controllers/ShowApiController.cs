using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Streaming1.DAL.Abstract;
using Streaming1.ExtensionMethods;
using Streaming1.Models;


namespace Streaming1.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ShowApiController : ControllerBase
    {
        private readonly ILogger<ShowApiController> _logger;
        private IRepository<Show> _showRepo;

        public ShowApiController(ILogger<ShowApiController> Logger, IRepository<Show> showRepo)
        {
            _logger = Logger;
            _showRepo = showRepo;
        }


       /* // GET: api/ShowApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Show>>> GetShows()
        {
            return await _context.Shows.ToListAsync();
        }

        // GET: api/ShowApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Show>> GetShow(int id)
        {
            var show = await _context.Shows.FindAsync(id);

            if (show == null)
            {
                return NotFound();
            }

            return show;
        }

        // PUT: api/ShowApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShow(int id, Show show)
        {
            if (id != show.Id)
            {
                return BadRequest();
            }

            _context.Entry(show).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowExists(id))
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

        // POST: api/ShowApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Show>> PostShow(Show show)
        {
            _context.Shows.Add(show);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShow", new { id = show.Id }, show);
        }

        // DELETE: api/ShowApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }

            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShowExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }*/

        // Routing /api/showAPI/actor/shows
        [HttpGet("actor/shows")]
        // Return async action result of list<ShowDTO> for shows
        public Task<ActionResult<Models.DTO.ShowDTO>> GetShows(int id)
        {
            try {
                // Attempt to find a show by its ID using the repository
                var shows = _showRepo.FindById(id);

                // Check if the show was found
                if (shows == null)
                {
                    return Task.FromResult<ActionResult<Models.DTO.ShowDTO>>(NotFound());
                }
                // Convert the found show to its DTO representation and return it
                return Task.FromResult<ActionResult<Models.DTO.ShowDTO>>(shows.toDTO());
            } catch (Exception ex) {
                _logger.LogError(ex, "Error in GetActor");
                return Task.FromResult<ActionResult<Models.DTO.ShowDTO>>(StatusCode(StatusCodes.Status500InternalServerError));
            }
        }

    }
}
