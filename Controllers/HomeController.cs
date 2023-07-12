using AgustinDonalisioProyectoPNT1.Data;
using AgustinDonalisioProyectoPNT1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return RedirectToAction("Index","DataIndex");
        }
        
        public IActionResult Privacy()
        {
            int totalWines = _context.Wines.Count(); 
            int totalUsers = _context.Users.Count();

            var viewModel = new PrivacyViewModel
            {
                TotalWines = totalWines
            };
            
            viewModel.TotalWines = totalWines;
            viewModel.TotalUsers = totalUsers;

            return View(viewModel);
        }

        public IActionResult Consumos()
        {
            return RedirectToAction("Index","Cellars");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}