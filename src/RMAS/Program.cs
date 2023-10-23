using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMAS.Data;
using RMAS.Interfaces;
using RMAS.Models;

namespace RMAS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string _RequireAuthenticatedUserPolicy = "Authenticated";

            // Add services to the container.

            // Default db connection set in appsettings.json.
            // Are both RMAS_dbContext and ApplicationDbContext necessary?
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<RMAS_dbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add framework services.
            builder.Services.AddDefaultIdentity<ApplicationUser>()// options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();

            // Configure bundling and minification
            builder.Services.AddWebOptimizer(pipeline =>
            {
                if (!builder.Environment.IsDevelopment())
                {
                    pipeline.MinifyCssFiles();
                    pipeline.MinifyJsFiles();
                }
            });

            // Configure authorization
            builder.Services.AddAuthorization(options => options.AddPolicy(_RequireAuthenticatedUserPolicy,
                        builder => builder.RequireAuthenticatedUser()));

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

            app.UseWebOptimizer();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            //MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}")
            app.MapDefaultControllerRoute().RequireAuthorization(_RequireAuthenticatedUserPolicy);
            app.MapRazorPages();

            app.Run();
        }        
    }
}
