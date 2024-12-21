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
        public Calisan user;
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
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRole");

            if (userId != null && userRole != null)
            {
                // Sadece adminler bu sayfaya eri�ebilir
                if (userRole == 1)
                {
                    return View();
                }
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Error = "Oturum bulunamad�. L�tfen tekrar giri� yap�n�z.";
                return RedirectToAction("Login", "Home");
            }


        }

        public IActionResult RolEkleme()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRole");

            if (userId != null && userRole != null)
            {
                // Sadece adminler bu sayfaya eri�ebilir
                if (userRole == 1)
                {

                    return View();
                }
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Error = "Oturum bulunamad�. L�tfen tekrar giri� yap�n�z.";
                return RedirectToAction("Login", "Home");
            }
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
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRole");

            if (userId != null && userRole != null)
            {
                // Sadece adminler bu sayfaya eri�ebilir
                if (userRole == 1)
                {
                    return View();
                }
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Error = "Oturum bulunamad�. L�tfen tekrar giri� yap�n�z.";
                return RedirectToAction("Login", "Home");
            }
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
        public IActionResult IslemEkleme()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRole");

            if (userId != null && userRole != null)
            {
                // Sadece adminler bu sayfaya eri�ebilir
                if (userRole == 1)
                {
                    ViewBag.SalonList = new SelectList(_context.Salons.ToList(), "SalonId", "SalonAdi");
                    return View();
                }
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Error = "Oturum bulunamad�. L�tfen tekrar giri� yap�n�z.";
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]


        public IActionResult IslemEkleme(Islem islem)
        {
            ViewBag.SalonList = new SelectList(_context.Salons.ToList(), "SalonId", "SalonAdi");


            try
            {
                // Veritaban�na ekle
                _context.Islems.Add(islem);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "��lem ba�ar�yla eklendi!";
                return RedirectToAction("IslemEkleme");
            }
            catch (Exception ex)
            {
                _logger.LogError("��lem eklenirken bir hata olu�tu: " + ex.Message);
                TempData["ErrorMessage"] = "Bir hata olu�tu: " + ex.Message;
            }


            return View(islem);
        }
        [HttpGet]
        public IActionResult CalisanEkleme()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRole");

            if (userId != null && userRole != null)
            {
                // Sadece adminler bu sayfaya eri�ebilir
                if (userRole == 1)
                {

                    ViewBag.SalonList = new SelectList(_context.Salons.ToList(), "SalonId", "SalonAdi");
                    ViewBag.IslemList = new SelectList(_context.Islems.ToList(), "IslemId", "IslemAdi");
                    ViewBag.RolList = new SelectList(_context.Rols.ToList(), "RolId", "RolAdi");
                    return View();
                }
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Error = "Oturum bulunamad�. L�tfen tekrar giri� yap�n�z.";
                return RedirectToAction("Login", "Home");
            }
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
                // Veritaban�na ekle
                _context.Calisans.Add(calisan);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "�al��an ba�ar�yla eklendi!";
                return RedirectToAction("CalisanEkleme");
            }
            catch (Exception ex)
            {
                _logger.LogError("�al��an eklenirken bir hata olu�tu: " + ex.Message);
                TempData["ErrorMessage"] = "Bir hata olu�tu: " + ex.Message;
            }

            return View(calisan);
        }

        public IActionResult Login(string username, string password)
        {
            user = _context.Calisans
         .FirstOrDefault(u => u.KullaniciAdi == username && u.Sifre == password);

            if (user != null)
            {
                // Kullan�c� bilgilerini oturuma ekle
                HttpContext.Session.SetInt32("UserId", user.CalisanId);
                HttpContext.Session.SetInt32("UserRole", user.RolId);

                // Admin paneline y�nlendirme
                if (user.RolId == 1)
                {
                    return RedirectToAction("AdminPanel", "Home");
                }
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.Error = "Kullan�c� ad� veya �ifre hatal�!";
                return View();
            }
        }


        // button eklenecek Laout'a
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // T�m oturum verilerini temizle
            return RedirectToAction("Index", "Home");
        }


        public IActionResult MusteriEkleme()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MusteriEkleme(Musteri musteri)
        {
            // Model do�rulama kontrol�

            try
            {
                // Yeni Musteri kayd�n� veritaban�na ekleme
                _context.Musteris.Add(musteri);

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
                return View(musteri);
            }


            // Model ge�ersizse ayn� sayfaya geri d�n
            return View(musteri);
        }


        public IActionResult RandevuEkleme()
        {
            // Oturum a�m�� kullan�c� var m� kontrol et
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "L�tfen giri� yap�n!";
                return RedirectToAction("Login"); // Kullan�c�y� giri� sayfas�na y�nlendir
            }
            else
            {
                return View();
            }

            return RedirectToAction("Login");
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuEkleme(string username, string password)
        {
            try
            {
                // Kullan�c�y� email ve �ifreye g�re ara
                var user = _context.Musteris
                    .FirstOrDefault(u => u.Email == username && u.MusteriSifre == password);

                if (user != null) // Kullan�c� bulundu
                {
                    // Oturuma kullan�c� bilgilerini ekle
                    HttpContext.Session.SetInt32("UserId", user.MusteriId);
                    HttpContext.Session.SetString("UserName", user.Email);

                    TempData["SuccessMessage"] = "Ba�ar�yla giri� yapt�n�z!";

                    // Giri� sonras� y�nlendirme
                    return RedirectToAction("RandevuEkleme"); // �rne�in ana sayfaya y�nlendirme
                }
                else
                {
                    TempData["ErrorMessage"] = "Email veya �ifre hatal�!";
                    return RedirectToAction("Login"); // Kullan�c�y� tekrar giri� sayfas�na y�nlendir
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Randevu al�rken bir hata olu�tu: {ex.Message}");
                TempData["ErrorMessage"] = "Bir hata olu�tu. L�tfen tekrar deneyin.";
                return RedirectToAction("Login"); // Hata durumunda login sayfas�na y�nlendirme
            }
        }
   

      
        public IActionResult RandevuGoruntuleme()
        {
            // Oturumdan kullan�c� ID'sini al
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "L�tfen giri� yap�n!";
                return RedirectToAction("Login");
            }

            // Kullan�c�ya ait randevular� getir
            var randevular = _context.Randevus
                .Include(r => r.Musteri)
                .Include(r => r.Calisan)
                .Include(r => r.Islem)
                .Where(r => r.MusteriId == userId)
                .ToList();

            return View(randevular);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
