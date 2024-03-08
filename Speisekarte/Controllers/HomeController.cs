using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Speisekarte.Data;
using Speisekarte.Models;
using System.Diagnostics;
using System.Globalization;

namespace Speisekarte.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        

        public IActionResult Index()
        {
            var speisenList =  _context.Speisen.Include(x => x.Zutaten).ToList();
            return View(speisenList);

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
        public async Task<IActionResult> Delete(int id)
        {
            SpeisenController speisenController = new SpeisenController(_context);
            return await speisenController.Delete(id);
        }
        public IActionResult CreateNewSpeise()
        {

            return View();
        }
    }
}
