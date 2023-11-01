using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

// Gunakan Startup.cs untuk konfigurasi
//builder.Services.AddRazorPages();
//builder.Services.AddControllers();


// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy",
        "default-src 'self' https://localhost:7084 wss://localhost:7084; " +
        "connect-src 'self' https://localhost:7084 wss://localhost:7084 wss://localhost:44369/WebApplication-MVC/; " +
        "script-src 'self' 'unsafe-inline' https://cdnjs.cloudflare.com; " +
        "style-src 'self' 'unsafe-inline'; " +
        "font-src 'self'; " +
        "img-src 'self'; " +
        "frame-src 'self'"
        );

    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
