using Microsoft.EntityFrameworkCore;
using WEB_PROGRAMLAMA_PROJESI_2024.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Veritaban� ba�lam� ekleniyor
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
/*
// CORS politikalar� ekleniyor
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // T�m kaynaklara izin ver
              .AllowAnyMethod() // GET, POST, PUT, DELETE gibi t�m metotlara izin ver
              .AllowAnyHeader(); // T�m ba�l�klara izin ver
    });
});

bunu a�arsan a�a��dakini de a�
*/
// Oturum hizmetleri ekleniyor
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi 30 dakika
    options.Cookie.HttpOnly = true;                // G�venlik i�in sadece HTTP ile eri�ilebilir
    options.Cookie.IsEssential = true;             // �erez, zorunlu olarak i�aretleniyor
    options.Cookie.Name = ".WEB_PROGRAMLAMA_PROJESI.Session"; // �erez ad� �zelle�tiriliyor
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseDeveloperExceptionPage(); // Geli�tirme ortam� i�in hata ay�klama sayfas�
}

app.UseStaticFiles();

app.UseRouting();

// CORS politikas� etkinle�tiriliyor
//app.UseCors("AllowAll");   // yukar�y� a�arsan bunu da a�

// Oturumlar etkinle�tiriliyor
app.UseSession();

app.UseAuthorization();

// Varsay�lan rota yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Anasayfa}/{id?}");

app.Run();
