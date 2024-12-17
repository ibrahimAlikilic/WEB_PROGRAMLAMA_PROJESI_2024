using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            // Model doðrulama kontrolü

                try
                {
                    // Yeni rol kaydýný veritabanýna ekleme
                    _context.Rols.Add(rol);

                    // Veritabanýna deðiþiklikleri kaydetme
                    _context.SaveChanges();

                    // Baþarý mesajýný TempData ile saklama
                    TempData["SuccessMessage"] = "Rol baþarýyla eklendi!";

                    // Rol listeleme veya baþka bir sayfaya yönlendirme
                    return RedirectToAction("RolEkleme");
                }
                catch (Exception ex)
                {
                    // Hata durumunda loglama (opsiyonel)
                    _logger.LogError("Rol eklenirken bir hata oluþtu: " + ex.Message);

                    // Hata mesajýný TempData ile saklama
                    TempData["ErrorMessage"] = "Bir hata oluþtu: " + ex.Message;

                    // Hata durumunda ayný form sayfasýna geri dönme
                    return View(rol);
                }
            

            // Model geçersizse ayný sayfaya geri dön
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
            // Model doðrulama kontrolü
           
                try
                {
                    // Yeni salon kaydýný veritabanýna ekleme
                    _context.Salons.Add(salon);

                    // Deðiþiklikleri veritabanýna kaydetme
                    _context.SaveChanges();

                    // Baþarý mesajýný TempData ile saklama
                    TempData["SuccessMessage"] = "Salon baþarýyla eklendi!";

                    // Baþka bir sayfaya veya ayný sayfaya yönlendirme
                    return RedirectToAction("SalonEkleme");
                }
                catch (Exception ex)
                {
                    // Hata durumunda loglama (opsiyonel)
                    _logger.LogError("Salon eklenirken bir hata oluþtu: " + ex.Message);

                    // Hata mesajýný TempData ile saklama
                    TempData["ErrorMessage"] = "Bir hata oluþtu: " + ex.Message;

                    // Hata durumunda ayný form sayfasýna geri dönme
                    return View(salon);
                }
            

            // Model geçersizse formu ayný sayfada göster
            return View(salon);
        }
        public IActionResult IslemEkleme()
        {
            ViewBag.SalonList = new SelectList(_context.Salons.ToList(), "SalonId", "SalonAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

       
        public IActionResult IslemEkleme(Islem islem)
        {
            ViewBag.SalonList = new SelectList(_context.Salons.ToList(), "SalonId", "SalonAdi");

           
                try
                {
                    // Veritabanýna ekle
                    _context.Islems.Add(islem);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Ýþlem baþarýyla eklendi!";
                    return RedirectToAction("IslemEkleme");
                }
                catch (Exception ex)
                {
                    _logger.LogError("Ýþlem eklenirken bir hata oluþtu: " + ex.Message);
                    TempData["ErrorMessage"] = "Bir hata oluþtu: " + ex.Message;
                }
           
        
            return View(islem);
        }
        [HttpGet]
        public IActionResult CalisanEkleme()
        {
            ViewBag.SalonList = new SelectList(_context.Salons.ToList(), "SalonId", "SalonAdi");
            ViewBag.IslemList = new SelectList(_context.Islems.ToList(), "IslemId", "IslemAdi");
            ViewBag.RolList = new SelectList(_context.Rols.ToList(), "RolId", "RolAdi");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CalisanEkleme(Calisan calisan)
        {
            ViewBag.SalonList = new SelectList(_context.Salons.ToList(), "SalonId", "SalonAdi");
            ViewBag.IslemList = new SelectList(_context.Islems.ToList(), "IslemId", "IslemAdi");
            ViewBag.RolList = new SelectList(_context.Rols.ToList(), "RolId", "RolAdi");

            try
            {
                // Veritabanýna ekle
                _context.Calisans.Add(calisan);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Çalýþan baþarýyla eklendi!";
                return RedirectToAction("CalisanEkleme");
            }
            catch (Exception ex)
            {
                _logger.LogError("Çalýþan eklenirken bir hata oluþtu: " + ex.Message);
                TempData["ErrorMessage"] = "Bir hata oluþtu: " + ex.Message;
            }

            return View(calisan);
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
