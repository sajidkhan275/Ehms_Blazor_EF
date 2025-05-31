using EHMSDB_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EHMSDB_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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


        // Scaffold-DbContext "Server=IN-LT-18043;Database=EHMS;User ID=sa;Password=1234;Encrypt=false; Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context EHMSDbContext -UseDatabaseNames -Force
    }
}
