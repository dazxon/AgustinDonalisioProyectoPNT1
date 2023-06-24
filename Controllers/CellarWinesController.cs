using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgustinDonalisioProyectoPNT1.Data;
using AgustinDonalisioProyectoPNT1.Models;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class CellarWinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CellarWinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CellarWines
        public async Task<IActionResult> Index()
        {
              return _context.CellarWines != null ? 
                          View(await _context.CellarWines.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CellarWines'  is null.");
        }

        // GET: CellarWines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CellarWines == null)
            {
                return NotFound();
            }

            var cellarWine = await _context.CellarWines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cellarWine == null)
            {
                return NotFound();
            }

            return View(cellarWine);
        }

        // GET: CellarWines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CellarWines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUser,IdWine,Quantity")] CellarWine cellarWine)
        {





            if (ModelState.IsValid)
            {
                _context.Add(cellarWine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cellarWine);
        }

        // GET: CellarWines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CellarWines == null)
            {
                return NotFound();
            }

            var cellarWine = await _context.CellarWines.FindAsync(id);
            if (cellarWine == null)
            {
                return NotFound();
            }
            return View(cellarWine);
        }

        // POST: CellarWines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUser,IdWine,Quantity")] CellarWine cellarWine)
        {
            if (id != cellarWine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cellarWine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CellarWineExists(cellarWine.Id))
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
            return View(cellarWine);
        }

        // GET: CellarWines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CellarWines == null)
            {
                return NotFound();
            }

            var cellarWine = await _context.CellarWines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cellarWine == null)
            {
                return NotFound();
            }

            return View(cellarWine);
        }

        // POST: CellarWines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CellarWines == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CellarWines'  is null.");
            }
            var cellarWine = await _context.CellarWines.FindAsync(id);
            if (cellarWine != null)
            {
                _context.CellarWines.Remove(cellarWine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CellarWineExists(int id)
        {
          return (_context.CellarWines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
