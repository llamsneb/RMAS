using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using RMAS.Data;
using RMAS.Models;
using RMAS.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RMAS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        static readonly string _RequireAuthenticatedUserPolicy = "Authenticated";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Default db connection set in appsettings.json.
            // Are both RMAS_dbContext and ApplicationDbContext necessary?
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RMAS_dbContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // Add framework services.
            services.AddDefaultIdentity<ApplicationUser>(
                    options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddControllersWithViews();
            services.AddRazorPages();//.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            // Add application services.
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
            // Register no-op EmailSender used by account confirmation and password reset 
            // during development
            //services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            // Configure authorization
            services.AddAuthorization(options => options.AddPolicy(_RequireAuthenticatedUserPolicy,
                        builder => builder.RequireAuthenticatedUser()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseCookiePolicy();
            // If the app uses Session or TempData based on Session:
            // app.UseSession();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseEndpoints(endpoints =>
            {
                //MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}")
                endpoints.MapDefaultControllerRoute().RequireAuthorization(_RequireAuthenticatedUserPolicy);
                endpoints.MapRazorPages();
            });
        }
    }
}
