﻿using AgustinDonalisioProyectoPNT1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("Index","Wines");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}