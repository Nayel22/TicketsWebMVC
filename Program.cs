using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketsWebMVC.Config;
using TicketsWebMVC.Data;
using TicketsWebMVC.Services;


var builder = WebApplication.CreateBuilder(args);

// Agregar HttpClient para consumir la API
builder.Services.AddHttpClient("TicketsAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7132/"); // La URL base de tu API
});

// Agregar configuración para la API
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Registrar el servicio genérico para la API
builder.Services.AddScoped(typeof(IApiService<>), typeof(ApiService<>));


// Agregar servicios de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Servicios de autenticación
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Y en la sección de configuración de app:
app.UseSession();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
