using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Streaming1.DAL.Abstract;
using Streaming1.DAL.Concrete;
using Streaming1.ExtensionMethods;
using Streaming1.Models;
using Streaming1.Models.DTO;

namespace Streaming1.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private IPersonRepository _personRepo;
        private StreamingDbContext _context;

        public AdminController(IPersonRepository personRepo, StreamingDbContext context)
        {
            _personRepo = personRepo;
            _context = context;
        }

        // GET: /admin
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.People.ToListAsync());
        }

        // GET: /admin/details/5
        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var person = await _context.People.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null) return NotFound();

            return View(person);
        }

        // GET: /admin/actor/create
        [HttpGet("actor/create")]
        public IActionResult Create()
        {
            return View();
        }

        // GET: /admin/person/create
        [HttpGet("person/create")]
        public IActionResult AddOrUpdate()
        {
            return View("AddorUpdate");
        }

        // POST: /admin/create
        [HttpPost("actor/create")]
        [ValidateAntiForgeryToken]
        // Parameter binding to avoid overposting 
        // Saves new person to database and returns view
        public async Task<IActionResult> Create([Bind("Id,JustWatchPersonId,FullName")] Person person)
        {
            // check if the justwatchpersonId is already in database
            bool exists = _context.People.Any(p => p.JustWatchPersonId == person.JustWatchPersonId);
            if (exists)
            {
                ModelState.AddModelError("JustWatchPersonId", "The JustWatchPersonId is already in use.");
                return View(person);
            }

            if (ModelState.IsValid)
            {
                // Add person to db and save changes asychronously
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: /admin/actor/edit/5
        [HttpGet("actor/edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var person = await _context.People.FindAsync(id);
            if (person == null) return NotFound();

            return View(person);
        }

        // POST: /admin/actor/edit/5
        [HttpPost("actor/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            // Keep JustWatchPersonID
            var existingPerson = await _context.People.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (existingPerson == null)
            {
                return NotFound();
            }

            person.JustWatchPersonId = existingPerson.JustWatchPersonId;

            if (ModelState.IsValid)
            {
                try
                {
                    // Update person to db and save changes asychronously
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: /admin/delete/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var person = await _context.People.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null) return NotFound();

            return View(person);
        }

        // POST: /admin/delete/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
