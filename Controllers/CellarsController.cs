using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgustinDonalisioProyectoPNT1.Data;
using AgustinDonalisioProyectoPNT1.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class CellarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CellarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cellars
        public async Task<IActionResult> Index()
        {
              return _context.Cellars != null ? 
                          View(await _context.Cellars.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Cellars'  is null.");
        }

        // GET: Cellars/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Cellars == null)
            {
                return NotFound();
            }

            var cellar = await _context.Cellars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cellar == null)
            {
                return NotFound();
            }

            return View(cellar);
        }

        // GET: Cellars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cellars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,IdUser,Description")] Cellar cellar)
        {

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = claim.Value;
                cellar.IdUser = userId;


            if (ModelState.IsValid)
            {


                _context.Add(cellar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cellar);
        }

        // GET: Cellars/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Cellars == null)
            {
                return NotFound();
            }

            var cellar = await _context.Cellars.FindAsync(id);
            if (cellar == null)
            {
                return NotFound();
            }
            return View(cellar);
        }

        // POST: Cellars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IdUser,Description")] Cellar cellar)
        {
            if (id != cellar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cellar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CellarExists(cellar.Id))
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
            return View(cellar);
        }

        // GET: Cellars/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Cellars == null)
            {
                return NotFound();
            }

            var cellar = await _context.Cellars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cellar == null)
            {
                return NotFound();
            }

            return View(cellar);
        }

        // POST: Cellars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Cellars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cellars'  is null.");
            }
            var cellar = await _context.Cellars.FindAsync(id);
            if (cellar != null)
            {
                _context.Cellars.Remove(cellar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CellarExists(int id)
        {
          return (_context.Cellars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
