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

// Oturum hizmetleri ekleniyor
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi 30 dakika
    options.Cookie.HttpOnly = true;                // G�venlik i�in sadece HTTP ile eri�ilebilir
    options.Cookie.IsEssential = true;             // �erez, zorunlu olarak i�aretleniyor
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Oturum kullan�m� ekleniyor
app.UseSession(); // <-- Oturumlar burada etkinle�tiriliyor

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Anasayfa}/{id?}");

app.Run();
