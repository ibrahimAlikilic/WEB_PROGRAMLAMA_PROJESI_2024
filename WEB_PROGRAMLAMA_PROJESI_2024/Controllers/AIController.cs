using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WEB_PROGRAMLAMA_PROJESI_2024.Controllers
{
    public class AIController : Controller
    {
        private const string ApiUrl = "https://www.ailabapi.com/api/image/effects/image-style-migration";
        private const string ApiKey = "kbMFSNOLT94DOG7F05knPEmxImXhNatY2RLtTJeosMUHwzUPcjvQj1GSf3HAdfhn";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyStyle(IFormFile contentImage, string selectedStyle)
        {
            if (contentImage == null || contentImage.Length == 0 || string.IsNullOrWhiteSpace(selectedStyle))
            {
                return BadRequest("Lütfen bir içerik görseli yükleyin ve stil seçin.");
            }

            var contentFilePath = Path.GetTempFileName();

            try
            {
                // İçerik görselini geçici dosyaya kaydet
                using (var stream = new FileStream(contentFilePath, FileMode.Create))
                {
                    await contentImage.CopyToAsync(stream);
                }

                // RestClient ve RestRequest oluştur
                var client = new RestClient(ApiUrl);
                var request = new RestRequest();

                // Header ve form-data ekle
                request.AddHeader("ailabapi-api-key", ApiKey);
                request.AddFile("major", contentFilePath);
                request.AddParameter("style", selectedStyle); // Stil seçimi metin olarak gönderiliyor

                request.Method = Method.Post;

                // API çağrısını yap
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful && response.Content != null)
                {
                    var jsonResponse = System.Text.Json.JsonDocument.Parse(response.Content);
                    var resultUrl = jsonResponse.RootElement.GetProperty("data").GetProperty("url").GetString();

                    return View("Result", resultUrl);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "API isteği başarısız oldu: " + response.Content);
                }
            }
            finally
            {
                // Geçici dosyayı temizle
                if (System.IO.File.Exists(contentFilePath))
                {
                    System.IO.File.Delete(contentFilePath);
                }
            }
        }
    }
}
