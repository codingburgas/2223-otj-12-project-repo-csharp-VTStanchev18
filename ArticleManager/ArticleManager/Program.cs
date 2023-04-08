using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArticleManager.Data;
app.UseAuthentication();;

internal class Program
{
    private static void Main(string[] args)
    {
        

        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllersWithViews();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}var connectionString = builder.Configuration.GetConnectionString("ArticleManagerContextConnection") ?? throw new InvalidOperationException("Connection string 'ArticleManagerContextConnection' not found.");

builder.Services.AddDbContext<ArticleManagerContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ArticleManagerUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ArticleManagerContext>();
