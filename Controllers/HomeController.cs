using PhoneTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace PhoneTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
            //return RedirectToAction("Login", "Account");
        }

        public IActionResult Track()
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
    }
}
