using AgustinDonalisioProyectoPNT1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: HistoryController
        public async Task<ActionResult> IndexAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _context.HistoryWines.Where(c => c.UserId == userId);
            var wines = await query.ToListAsync();
            return View(wines);
        }

    }
}
