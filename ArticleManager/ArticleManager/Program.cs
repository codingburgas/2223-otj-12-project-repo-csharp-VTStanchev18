using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArticleManager.Data;
using ArticleManager.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;

//

internal class Program
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add database context
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Add controllers and views
        services.AddControllersWithViews();
    }


    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllersWithViews();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();
        app.UseAuthentication();;
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