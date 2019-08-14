using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Week3App.Models;

namespace Week3App.Controllers
{
    public class MovieTblesController : Controller
    {
        private readonly MoviesContext _context;

        public MovieTblesController(MoviesContext context)
        {
            _context = context;
        }

        // GET: MovieTbles
        public async Task<IActionResult> Index()
        {
            var moviesContext = _context.MovieTble.Include(m => m.CategoryNavigation);
            return View(await moviesContext.ToListAsync());
        }

        // GET: MovieTbles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTble = await _context.MovieTble
                .Include(m => m.CategoryNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieTble == null)
            {
                return NotFound();
            }

            return View(movieTble);
        }

        // GET: MovieTbles/Create
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_context.CategoryTble, "Code", "Code");
            return View();
        }

        // POST: MovieTbles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ReleaseDate,Director,Email,Language,Category")] MovieTble movieTble)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieTble);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = new SelectList(_context.CategoryTble, "Code", "Code", movieTble.Category);
            return View(movieTble);
        }

        // GET: MovieTbles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTble = await _context.MovieTble.FindAsync(id);
            if (movieTble == null)
            {
                return NotFound();
            }
            ViewData["Category"] = new SelectList(_context.CategoryTble, "Code", "Code", movieTble.Category);
            return View(movieTble);
        }

        // POST: MovieTbles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseDate,Director,Email,Language,Category")] MovieTble movieTble)
        {
            if (id != movieTble.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieTble);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieTbleExists(movieTble.Id))
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
            ViewData["Category"] = new SelectList(_context.CategoryTble, "Code", "Code", movieTble.Category);
            return View(movieTble);
        }

        // GET: MovieTbles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTble = await _context.MovieTble
                .Include(m => m.CategoryNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieTble == null)
            {
                return NotFound();
            }

            return View(movieTble);
        }

        // POST: MovieTbles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieTble = await _context.MovieTble.FindAsync(id);
            _context.MovieTble.Remove(movieTble);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieTbleExists(int id)
        {
            return _context.MovieTble.Any(e => e.Id == id);
        }
    }
}
