using Microsoft.AspNetCore.Mvc;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class DataIndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
