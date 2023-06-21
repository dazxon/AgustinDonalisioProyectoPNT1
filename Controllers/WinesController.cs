using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgustinDonalisioProyectoPNT1.Data;
using AgustinDonalisioProyectoPNT1.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class WinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Wines
        public async Task<IActionResult> Index(string searchTerm)
        {
            string userId = User.Identity.Name;

            var query = _context.Wines
                .Where(w => w.IdUser == userId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(w => w.Brand.Contains(searchTerm) || w.Name.Contains(searchTerm) || w.Type.Contains(searchTerm));
            }

            var wines = await query
                .OrderByDescending(w => w.ConsumptionDate)
                .ToListAsync();

            var totalWinesConsumed = await _context.Wines
                .CountAsync(w => w.IdUser == userId);

            ViewBag.TotalWinesConsumed = totalWinesConsumed;

            return View(wines);
        }


        // GET: Wines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wines == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // GET: Wines/Create
        public IActionResult Create()
        {
            Wine wine = new Wine();
            wine.ConsumptionDate = DateTime.Now;
            return View(wine);
        }

        // POST: Wines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Brand,Name,Type,ConsumptionDate,Description,Price,Notes")] Wine wine)
        {
            if (ModelState.IsValid)
            {
                wine.IdUser = User.Identity.Name;
                wine.CreatedAt = DateTime.Now;
                wine.UpdatedAt = DateTime.Now;

                if (!string.IsNullOrEmpty(wine.Brand))
                {
                    wine.Brand = wine.Brand.ToUpper();
                }
                else
                {
                    var existingWineWithSameNameAndBrand = await _context.Wines.FirstOrDefaultAsync(w => w.Name == wine.Name && w.Brand != null);
                    if (existingWineWithSameNameAndBrand != null)
                    {
                        wine.Brand = existingWineWithSameNameAndBrand.Brand.ToUpper();
                    }
                    
                }

                if (!string.IsNullOrEmpty(wine.Name))
                {
                    wine.Name = wine.Name.ToUpper();
                }
                else
                {
                    var existingWineWithSameNameAndType = await _context.Wines.FirstOrDefaultAsync(w => w.Name == wine.Name && w.Type != null);
                    if (existingWineWithSameNameAndType != null)
                    {
                        wine.Name = existingWineWithSameNameAndType.Name.ToUpper();
                    }
                }

                if (!string.IsNullOrEmpty(wine.Type))
                {
                    wine.Type = wine.Type.ToUpper();
                }
                else
                {
                    var existingWineWithSameNameAndType = await _context.Wines.FirstOrDefaultAsync(w => w.Name == wine.Name && w.Type != null);
                    if (existingWineWithSameNameAndType != null)
                    {
                        wine.Type = existingWineWithSameNameAndType.Type.ToUpper();
                    }
                }

                if (string.IsNullOrEmpty(wine.ConsumptionDate.ToString()))
                {
                    wine.ConsumptionDate = DateTime.Now;
                }

                _context.Add(wine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(wine);
        }

        // GET: Wines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wines == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines.FindAsync(id);
            
            if (wine == null)
            {
                return NotFound();
            }
            return View(wine);
        }

        // POST: Wines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Name,Type,Description,Price,Notes")] Wine wine)
        {
            if (id != wine.Id)
            {
                return NotFound();
            }
            wine.Id = id;
            wine.IdUser = User.Identity.Name;
            wine.UpdatedAt = DateTime.Now;

            if (!string.IsNullOrEmpty(wine.Brand))
            {
                wine.Brand = wine.Brand.ToUpper();
            }
            else
            {
                var existingWineWithSameNameAndBrand = await _context.Wines.FirstOrDefaultAsync(w => w.Name == wine.Name && w.Brand != null);
                if (existingWineWithSameNameAndBrand != null)
                {
                    wine.Brand = existingWineWithSameNameAndBrand.Brand.ToUpper();
                }

            }

            if (!string.IsNullOrEmpty(wine.Name))
            {
                wine.Name = wine.Name.ToUpper();
            }

            if (!string.IsNullOrEmpty(wine.Type))
            {
                wine.Type = wine.Type.ToUpper();
            }
            else
            {
                var existingWineWithSameNameAndType = await _context.Wines.FirstOrDefaultAsync(w => w.Name == wine.Name && w.Type != null);
                if (existingWineWithSameNameAndType != null)
                {
                    wine.Type = existingWineWithSameNameAndType.Type.ToUpper();
                }
            }


            if (string.IsNullOrEmpty(wine.ConsumptionDate.ToString()))
            {
                wine.ConsumptionDate = DateTime.Now;
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var existingWine = await _context.Wines.FindAsync(wine.Id);
                    if (existingWine == null)
                    {
                        return NotFound();
                    }

                    _context.Entry(existingWine).CurrentValues.SetValues(wine);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WineExists(wine.Id))
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

            return View(wine);
        }

        // GET: Wines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Wines == null)
            {
                return NotFound();
            }

            var wine = await _context.Wines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wine == null)
            {
                return NotFound();
            }

            return View(wine);
        }

        // POST: Wines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wines == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Wines'  is null.");
            }
            var wine = await _context.Wines.FindAsync(id);
            if (wine != null)
            {
                _context.Wines.Remove(wine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WineExists(int id)
        {
          return (_context.Wines?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
