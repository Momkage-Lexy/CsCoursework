using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Streaming1.Models;

namespace Streaming1.Controllers
{
    public class ShowController : Controller
    {
        private readonly StreamingDbContext _context;

        public ShowController(StreamingDbContext context)
        {
            _context = context;
        }

        // GET: Show
        public async Task<IActionResult> Index()
        {
            var streamingDbContext = _context.Shows.Include(s => s.AgeCertification).Include(s => s.ShowType);
            return View(await streamingDbContext.ToListAsync());
        }

        // GET: Show/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows
                .Include(s => s.AgeCertification)
                .Include(s => s.ShowType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // GET: Show/Create
        public IActionResult Create()
        {
            ViewData["AgeCertificationId"] = new SelectList(_context.AgeCertifications, "Id", "Id");
            ViewData["ShowTypeId"] = new SelectList(_context.ShowTypes, "Id", "Id");
            return View();
        }

        // POST: Show/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JustWatchShowId,Title,Description,ShowTypeId,ReleaseYear,AgeCertificationId,Runtime,Seasons,ImdbId,ImdbScore,ImdbVotes,TmdbPopularity,TmdbScore")] Show show)
        {
            if (ModelState.IsValid)
            {
                _context.Add(show);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgeCertificationId"] = new SelectList(_context.AgeCertifications, "Id", "Id", show.AgeCertificationId);
            ViewData["ShowTypeId"] = new SelectList(_context.ShowTypes, "Id", "Id", show.ShowTypeId);
            return View(show);
        }

        // GET: Show/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }
            ViewData["AgeCertificationId"] = new SelectList(_context.AgeCertifications, "Id", "Id", show.AgeCertificationId);
            ViewData["ShowTypeId"] = new SelectList(_context.ShowTypes, "Id", "Id", show.ShowTypeId);
            return View(show);
        }

        // POST: Show/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JustWatchShowId,Title,Description,ShowTypeId,ReleaseYear,AgeCertificationId,Runtime,Seasons,ImdbId,ImdbScore,ImdbVotes,TmdbPopularity,TmdbScore")] Show show)
        {
            if (id != show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(show.Id))
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
            ViewData["AgeCertificationId"] = new SelectList(_context.AgeCertifications, "Id", "Id", show.AgeCertificationId);
            ViewData["ShowTypeId"] = new SelectList(_context.ShowTypes, "Id", "Id", show.ShowTypeId);
            return View(show);
        }

        // GET: Show/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows
                .Include(s => s.AgeCertification)
                .Include(s => s.ShowType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // POST: Show/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show != null)
            {
                _context.Shows.Remove(show);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }
    }
}
