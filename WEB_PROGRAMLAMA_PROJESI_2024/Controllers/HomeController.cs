using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_PROGRAMLAMA_PROJESI_2024.Models;

namespace WEB_PROGRAMLAMA_PROJESI_2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Anasayfa()
        {
            return View();
        }

        public IActionResult Tarihce()
        {
            return View();
        }
        public IActionResult AdminPanel()
        {

            return View();
        }
        public IActionResult Login(string username, string password)
        {
            if (username == "b211210091@sakarya.edu.tr" && password == "sau")
            {
                return RedirectToAction("AdminPanel");
            }
            else
            {
                ViewBag.Error = "Kullanýcý adý veya þifre hatalý.";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
