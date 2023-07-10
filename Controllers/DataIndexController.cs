using AgustinDonalisioProyectoPNT1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace AgustinDonalisioProyectoPNT1.Controllers
{
    public class DataIndexController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DataIndexController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            var historyWines = await _context.HistoryWines.ToListAsync();

            var wineGroupsYear = new Dictionary<string, int>();
            var wineGroupsMonth = new Dictionary<string, int>();

            string mostFrequentWineYear = null;
            string mostFrequentWineMonth = null;

            int maxCountYear = 0;
            int maxCountMonth = 0;

            foreach (var wine in historyWines)
            {
                if (wine.Consumed.Year == currentYear)
                {
                    if (wineGroupsYear.ContainsKey(wine.WineName))
                    {
                        wineGroupsYear[wine.WineName]++;
                    }
                    else
                    {
                        wineGroupsYear[wine.WineName] = 1;
                    }

                    if (wineGroupsYear[wine.WineName] > maxCountYear)
                    {
                        maxCountYear = wineGroupsYear[wine.WineName];
                        mostFrequentWineYear = wine.WineName;
                    }

                    if (wine.Consumed.Month == currentMonth)
                    {
                        if (wineGroupsMonth.ContainsKey(wine.WineName))
                        {
                            wineGroupsMonth[wine.WineName]++;
                        }
                        else
                        {
                            wineGroupsMonth[wine.WineName] = 1;
                        }

                        if (wineGroupsMonth[wine.WineName] > maxCountMonth)
                        {
                            maxCountMonth = wineGroupsMonth[wine.WineName];
                            mostFrequentWineMonth = wine.WineName;
                        }
                    }
                }
            }

            ViewBag.MostFrequentWineYear = mostFrequentWineYear;
            ViewBag.MostFrequentWineYearCount = maxCountYear;
            ViewBag.MostFrequentWineMonth = mostFrequentWineMonth;
            ViewBag.MostFrequentWineMonthCount = maxCountMonth;

            return View();
        }
    }
}
