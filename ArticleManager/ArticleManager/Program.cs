using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArticleManager.Data;
using ArticleManager.Models;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        
        IServiceCollection services = builder.Services;
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));
            options.AddPolicy("EditorOrAdmin", policy =>
                policy.RequireRole("Editor", "Admin"));
        });
        services.AddAuthentication("Cookies")
        .AddCookie("Cookies", options =>
        {
            options.Cookie.Name = "MyApp.Cookie";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
        });
        
        services.AddControllersWithViews();
        builder.Services.AddControllersWithViews();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();
        app.UseAuthentication();;
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute(
                name: "user",
                pattern: "{controller=User}/{action=Register}/{id?}");
        });

    }
}