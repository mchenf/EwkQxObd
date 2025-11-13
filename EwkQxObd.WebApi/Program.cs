using EwkQxObd.WebApi.Data;
using EwkQxObd.WebApi.Models.IqxApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace EwkQxObd.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromSeconds(10);
                opts.Cookie.HttpOnly = true;
                opts.Cookie.IsEssential = true;
            });

            builder.Services.AddSingleton<AuthClient>();

            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddMvc();

            builder.Services.AddDbContext<EwkIqxObdContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
#if true
                Debug.WriteLine($"Connection String is: \r\n${builder.Configuration.GetConnectionString("DefaultConnection")}");
                options.EnableSensitiveDataLogging();
#endif
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"

            );

            app.UseSession();

            app.UseStaticFiles();
            app.MapControllers();


            app.Run();
        }
    }
}
