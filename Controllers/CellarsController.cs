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
using AgustinDonalisioProyectoPNT1.Models.ViewModel;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Collections;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class CellarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CellarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Cellars
        public async Task<IActionResult> Index(string searchTerm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _context.Cellars.Where(c => c.IdUser == userId);
            ViewBag.SearchTerm = searchTerm;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.Name.Contains(searchTerm));
            }

            var cellars = await query.ToListAsync();
            return View(cellars);
        }


        // GET: Cellars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cellars == null)
            {
                return NotFound();
            }

            var cellar = await _context.Cellars.FirstOrDefaultAsync(m => m.Id == id);
            if (cellar == null)
            {
                return NotFound();
            }

            var cellarWines = _context.CellarWines.Where(m => m.IdCellar == id).ToList();
            var wines = new List<CreateWineModel>();

            cellarWines.ForEach(e =>
            {
                var wine = _context.Wines.FirstOrDefault(m => m.Id == e.IdWine);

                var aux = new CreateWineModel
                {
                    Name = wine.Name,
                    IdWine = wine.Id,
                    Year = wine.Year,
                    Brand = wine.Brand,
                    WineQuantity = e.Quantity,
                    Type = wine.Type,
                    IdCellar = (int)id
                };

                wines.Add(aux);
            });

            ViewBag.Wines = wines;

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
        public async Task<IActionResult> Create([Bind("Id,Name,IdUser,Description")] Cellar cellar)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var idUser = claim.Value;
                cellar.IdUser = idUser;
                cellar.Name = cellar.Name.ToUpper();

                if (cellar.Description != null)
                {
                    cellar.Description = cellar.Description.ToUpper();
                }

                _context.Add(cellar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cellar);
        }

        // GET: Cellars/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            cellar.Name = cellar.Name.ToUpper();

            if (cellar.Description != null)
            {
                cellar.Description = cellar.Description.ToUpper();
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
                return RedirectToAction("Details", new { id = cellar.Id });
            }
            return View(cellar);
        }

        // GET: Cellars/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
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


        [HttpPost]
        public IActionResult CheckWine(string name, int cellarId)
        {
            Console.WriteLine(cellarId);
            var wine = _context.Wines.FirstOrDefault(e => e.Name == name);
            CreateWineModel model = new CreateWineModel();
            model.IdCellar = cellarId;

            if (wine != null)
            {
                model.Name = wine.Name.ToUpper();
                model.Brand = wine.Brand.ToUpper();
                model.Year = wine.Year;
                model.Type = wine.Type.ToUpper();
                model.WineQuantity = 1;
                return View("AddWine", model);
            }

            return View("AddWine", model);
        }

        // GET: Cellars/AddWine?CellarId=
        public IActionResult AddWine(int cellarId)
        {
            CreateWineModel model = new CreateWineModel();
            model.IdCellar = cellarId;
            return View(model);
        }



        // POST: Cellars/AddWine?CellarId=
        public async Task<IActionResult> AddWineTwo(CreateWineModel model)
        {
            if (ModelState.IsValid)
            {
                Wine wine = await _context.Wines.FirstOrDefaultAsync(e => e.Name == model.Name);
                if (wine == null)
                {
                    wine = new Wine
                    {
                        Name = model.Name.ToUpper(),
                        Brand = model.Brand.ToUpper(),
                        Year = model.Year,
                        Type = model.Type.ToUpper()
                    };
                    _context.Wines.Add(wine);
                    await _context.SaveChangesAsync();
                }

                CellarWine cellarWine = await _context.CellarWines.FirstOrDefaultAsync(e => e.IdWine == wine.Id && e.IdCellar == model.IdCellar);
                if (cellarWine == null)
                {
                    cellarWine = new CellarWine
                    {
                        IdWine = wine.Id,
                        IdCellar = model.IdCellar,
                        Quantity = model.WineQuantity
                    };
                    _context.CellarWines.Add(cellarWine);
                }
                else
                {
                    cellarWine.Quantity += model.WineQuantity;
                    _context.CellarWines.Update(cellarWine);
                }

                Cellar cellar = await _context.Cellars.FirstOrDefaultAsync(e => e.Id == model.IdCellar);
                if (cellar != null)
                {
                    cellar.WineQuantity += model.WineQuantity;
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = model.IdCellar });
        }

        public async Task<IActionResult> AddToWineAsync(int IdCellar,int IdWine)
        {

            CellarWine cellarWine = await _context.CellarWines.FirstOrDefaultAsync(e => e.IdCellar == IdCellar && e.IdWine == IdWine);
            cellarWine.Quantity++;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = IdCellar });
        }

        public async Task<IActionResult> ResToWineAsync(int IdCellar, int IdWine)
        {

            CellarWine cellarWine = await _context.CellarWines.FirstOrDefaultAsync(e => e.IdCellar == IdCellar && e.IdWine == IdWine);
            cellarWine.Quantity--;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = IdCellar });
        }


    }
}
