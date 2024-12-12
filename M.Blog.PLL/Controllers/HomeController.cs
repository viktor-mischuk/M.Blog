using M.Blog.PLL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M.Blog.PLL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }

        public IActionResult Index()
        {
            HttpContext context = this.HttpContext;
            _logger.LogInformation("Моя информация!");
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

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult ResourceNotFound()
        {
            return View();
        }

        public IActionResult SomethingWrong()
        {
            return View();
        }
    }
}
