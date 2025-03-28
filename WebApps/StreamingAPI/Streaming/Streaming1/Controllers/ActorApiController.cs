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
using Streaming1.Models.DTO;

namespace Streaming1.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ActorApiController : ControllerBase
    {
        private readonly ILogger<ActorApiController> _logger;
        private IRepository<Person> _personRepo;

        public ActorApiController(ILogger<ActorApiController> Logger, IRepository<Person> personRepo)
        {
            _logger = Logger;
            _personRepo = personRepo;
        }

        // GET: api/ActorApi
      /*  [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            return await _context.People.ToListAsync();
        }

        // GET: api/ActorApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/ActorApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // DELETE: api/ActorApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }*/

        // Routing /admin/search/actor
        [HttpGet("search/actor")]
        // Return async action result of list<ActorDTO> for actors
        public Task<ActionResult<Models.DTO.ActorDTO>> GetActor (string name)
        {
            try {
                // get all people from person dbset
                var actors = _personRepo.GetAll()
                // if their fullname contains the parameter name
                    .Where(p => p.FullName.Contains(name))
                    // select and map to DTO IF their credited as an actor
                    .Select(p => new ActorDTO
                    {
                        Id = p.Id,
                        FullName = p.FullName,
                        ShowIds = p.Credits
                            .Where(c => c.RoleId == 1)  // Filter credits with RoleId == 1
                            .Select(c => c.ShowId)       // Get ShowId from each matching credit
                            .ToList()
                    })
                    .ToList();
                if(actors == null)
                {
                    return Task.FromResult<ActionResult<ActorDTO>>(NotFound());
                }

                return Task.FromResult<ActionResult<ActorDTO>>(Ok(actors));
            } catch (Exception ex) {
                _logger.LogError(ex, "Error in GetActor");
                return Task.FromResult<ActionResult<ActorDTO>>(StatusCode(StatusCodes.Status500InternalServerError));
            }
        }
    }
}
