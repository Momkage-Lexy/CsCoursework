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
    [Route("api/actor")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly ILogger<AdminApiController> _logger;
        private IPersonRepository _personRepo;

        public AdminApiController(ILogger<AdminApiController> logger, IPersonRepository personRepo)
        {
            _logger = logger;
            _personRepo = personRepo;
        }
            
        // PUT: api/AdminApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Task<IActionResult> PutPerson(int id, Streaming1.Models.DTO.ActorDTO person)
        {
            if (id != person.Id)
            {
                return Task.FromResult<IActionResult>(Problem(detail: "Invalid ID", statusCode: 400));
            }

            // New object
            Person personEntity;
            // Create new person
            if (person.Id == 0)
            {
                // Add new person
                // Make sure new person cannot use someone elses Justwatchpersonid 
                if(_personRepo.JustWatchPersonIdAlreadyInUse(person.JustWatchPersonId))
                {
                    return Task.FromResult<IActionResult>(Problem(detail: "The JustWatchPersonId is already in use. Please enter a different ID", statusCode: 400));
                }
                // Map to DTO
                personEntity = person.ToActor();
            }
            else
            {
                // Update existing person
                // Find person by ID
                personEntity = _personRepo.FindById(person.Id);
                if (personEntity == null)
                {
                    return Task.FromResult<IActionResult>(Problem(detail: "Invalid Id", statusCode: 400));
                }
                // Update just the name property
                personEntity.FullName = person.FullName;
            }
            try
            {
                // Add or Update person in db
                _personRepo.AddOrUpdate(personEntity);
            }
            catch(DbUpdateConcurrencyException)
            {
                return Task.FromResult<IActionResult>(Problem(detail: "The server experienced a problem.  Please try again.", statusCode: 500));
            }
            catch(DbUpdateException)
            {
                return Task.FromResult<IActionResult>(Problem(detail: "The server experienced a problem.  Please try again.", statusCode: 500));
            }
            if (id == 0)
            {
                return Task.FromResult<IActionResult>(CreatedAtAction("GetPerson", new { id = person.JustWatchPersonId}, person));
            }
            else
            {
                return Task.FromResult<IActionResult>(NoContent());
            }

        }
/*
        // POST: api/AdminApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _personRepo.People.Add(person);
            await _personRepo.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }


        private bool PersonExists(int id)
        {
            return _personRepo.People.Any(e => e.Id == id);
        }*/

        // GET: api/person/people
        [HttpGet("people")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Streaming1.Models.DTO.ActorDTO>))]
        public ActionResult<IEnumerable<Streaming1.Models.DTO.ActorDTO>> GetPeople()
        {
            var people = _personRepo.GetAll()
                                           .Select(s => s.toDTO())
                                           .ToList();
            return people;
        }

        // GET: api/person/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Streaming1.Models.DTO.ActorDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Streaming1.Models.DTO.ActorDTO> GetActor(int id)
        {

            var person = _personRepo.FindById(id);
            if (person == null)
            {
                return NotFound();
            }

            return person.toDTO();
        }

        // DELETE: api/person/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePerson(int id)
        {
            var person = _personRepo.FindById(id);
            if (person == null)
            {
                return NotFound();
            }

            try
            {
                _personRepo.Delete(person);
            }
            catch (Exception)
            {
                return Problem(detail: "The server experienced a problem.  Please try again.", statusCode: 500);
            }

            return NoContent();
        }
    }
}
