using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WEB_PROGRAMLAMA_PROJESI_2024.Data;
using WEB_PROGRAMLAMA_PROJESI_2024.Models;

namespace WEB_PROGRAMLAMA_PROJESI_2024.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context; // DbContext
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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

        public IActionResult RolEkleme()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RolEkleme(Rol rol)
        {
            // Model do�rulama kontrol�

                try
                {
                    // Yeni rol kayd�n� veritaban�na ekleme
                    _context.Rols.Add(rol);

                    // Veritaban�na de�i�iklikleri kaydetme
                    _context.SaveChanges();

                    // Ba�ar� mesaj�n� TempData ile saklama
                    TempData["SuccessMessage"] = "Rol ba�ar�yla eklendi!";

                    // Rol listeleme veya ba�ka bir sayfaya y�nlendirme
                    return RedirectToAction("RolEkleme");
                }
                catch (Exception ex)
                {
                    // Hata durumunda loglama (opsiyonel)
                    _logger.LogError("Rol eklenirken bir hata olu�tu: " + ex.Message);

                    // Hata mesaj�n� TempData ile saklama
                    TempData["ErrorMessage"] = "Bir hata olu�tu: " + ex.Message;

                    // Hata durumunda ayn� form sayfas�na geri d�nme
                    return View(rol);
                }
            

            // Model ge�ersizse ayn� sayfaya geri d�n
            return View(rol);
        }


        public IActionResult SalonEkleme()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SalonEkleme(Salon salon)
        {
            // Model do�rulama kontrol�
           
                try
                {
                    // Yeni salon kayd�n� veritaban�na ekleme
                    _context.Salons.Add(salon);

                    // De�i�iklikleri veritaban�na kaydetme
                    _context.SaveChanges();

                    // Ba�ar� mesaj�n� TempData ile saklama
                    TempData["SuccessMessage"] = "Salon ba�ar�yla eklendi!";

                    // Ba�ka bir sayfaya veya ayn� sayfaya y�nlendirme
                    return RedirectToAction("SalonEkleme");
                }
                catch (Exception ex)
                {
                    // Hata durumunda loglama (opsiyonel)
                    _logger.LogError("Salon eklenirken bir hata olu�tu: " + ex.Message);

                    // Hata mesaj�n� TempData ile saklama
                    TempData["ErrorMessage"] = "Bir hata olu�tu: " + ex.Message;

                    // Hata durumunda ayn� form sayfas�na geri d�nme
                    return View(salon);
                }
            

            // Model ge�ersizse formu ayn� sayfada g�ster
            return View(salon);
        }

        public IActionResult Login(string username, string password)
        {
            if (username == "b211210091@sakarya.edu.tr" && password == "sau")
            {
                return RedirectToAction("AdminPanel");
            }
            else
            {
                ViewBag.Error = "Kullan�c� ad� veya �ifre hatal�.";
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
