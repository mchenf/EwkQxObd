using EwkQxObd.WebApi.Authorization;
using EwkQxObd.WebApi.Data;
using EwkQxObd.WebApi.Data.FossApi;
using EwkQxObd.WebApi.Models.IqxApi;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            builder.Services.AddHttpClient();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromSeconds(3660);
                opts.Cookie.HttpOnly = true;
                opts.Cookie.IsEssential = true;
                opts.Cookie.SameSite = SameSiteMode.Lax;
                opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddSingleton<AuthClient>();

            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddMvc();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/IqxApi/Login";          // 未登录时跳转的地址
                options.LogoutPath = "/IqxApi/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(7); // Cookie 过期时间
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddScoped<LoginManager>();
            builder.Services.AddScoped<CommonBFF>();

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


            app.UseSession();
            app.UseAuthentication();
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
