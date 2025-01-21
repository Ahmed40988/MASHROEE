﻿using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MASHROEE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            
            builder.Services.AddDbContext<MASHROEEDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        
            // reigster usermanger and Rolemanager services and add Identity Stors (Database services)
            builder.Services.AddIdentity<Applicationuser, IdentityRole>(options=>
            {

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 4;
            }
            )
                .AddEntityFrameworkStores<MASHROEEDbContext>();


            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");


           
            app.Run();
        }
    }
}
