using EwkQxObd.WebApi.Data;
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

            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddMvc();

            builder.Services.AddDbContext<EwkIqxObdContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EwkQxObd"));
#if true
                Debug.WriteLine($"Connection String is: \r\n${builder.Configuration.GetConnectionString("EwkQxObd")}");
#endif
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"

            );

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
