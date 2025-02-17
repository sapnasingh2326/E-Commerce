using ECommerceWeb.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Http.Features;
using static System.Net.Mime.MediaTypeNames;
using ECommerceWeb.Services;

namespace ECommerceWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();

            // Add DbContext service
            builder.Services.AddDbContext<DemoProjectContext>(options =>
            
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            // Add scoped service
            builder.Services.AddScoped<INewArrivalProductService, NewArrivalProductService>();

            //------Dynamic Page-----
            builder.Services.AddScoped<INewInformationDynamicPageService, NewInformationDynamicPageService>();


            // Add session services
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //-------------multipart for file uploads--------
            builder.Services.Configure<FormOptions>(options =>
            {
                // Set the maximum allowed size of multipart body (for file uploads) in bytes
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });


            // Add EmailSender as a service (This is where you add the EmailSender service)
            builder.Services.AddTransient<EmailSender>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // UseSession should be called before UseAuthorization and UseEndpoints
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.Run();

        }

    }
}
