using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text;

namespace WEB_PROGRAMLAMA_PROJESI_2024.Controllers
{
    public class AIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ApplyHairStyle(IFormFile imageFile, string hairStyle)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Lütfen geçerli bir görsel yükleyin.");
            }

            // Görseli geçici bir dosyaya kaydet
            var tempFilePath = Path.GetTempFileName();
            try
            {
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // API İsteği
                var options = new RestClientOptions("https://www.ailabapi.com/api/portrait/effects/hairstyle-editor") //https://www.ailabapi.com
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/portrait/effects/hairstyle-editor", Method.Post);
                request.AddHeader("ailabapi-api-key", "kbMFSNOLT94DOG7F05knPEmxImXhNatY2RLtTJeosMUHwzUPcjvQj1GSf3HAdfhn");
                request.AlwaysMultipartFormData = true;
                request.AddFile("image_target", tempFilePath);
                request.AddParameter("hair_type", hairStyle);

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    var jsonResponse = System.Text.Json.JsonDocument.Parse(response.Content);
                    var base64Image = jsonResponse.RootElement.GetProperty("data").GetProperty("image").GetString();

                    // Base64 Görseli çöz ve View'e gönder
                    var imageBytes = Convert.FromBase64String(base64Image);
                    var imageDataUrl = $"data:image/png;base64,{base64Image}";

                    return View("Result", imageDataUrl);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "API isteği başarısız oldu: " + response.Content);
                }
            }
            finally
            {
                // Geçici dosyayı temizle
                if (System.IO.File.Exists(tempFilePath))
                {
                    System.IO.File.Delete(tempFilePath);
                }
            }
        }
    }
}
