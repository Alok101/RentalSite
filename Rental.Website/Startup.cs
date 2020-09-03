using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rental.Website.Infrastructures;
using Rental.Website.Models;
using System.Security.Claims;

namespace Rental.Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomDb(Configuration);
            services.AddCustomIdentity();
            services.Configure<ConfigureServiceModel>(Configuration.GetSection("MySettings"));
            services.AddExternalAuth();
            services.AddControllersWithViews();
            services.AddMvc()
            .AddSessionStateTempDataProvider();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    //Custom Extension Method
    public static class CustomExtensionMethod
    {
        public static IServiceCollection AddCustomDb(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContextPool<SSODbContext>
            (
            options => options.UseSqlServer(configuration.GetConnectionString("RentalSSODBConnection"))
            );
            return services;
        }
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                //options.Password.RequireNonAlphanumeric = false;
            })
               .AddEntityFrameworkStores<SSODbContext>();
            return services;
        }
        public static IServiceCollection AddExternalAuth(this IServiceCollection services)
        {
            services.AddAuthentication()
             .AddGoogle(options =>
             {
                 options.ClientId = "196275387351-has575ff9m45ka8ld17s0qtps42jkmtt.apps.googleusercontent.com";
                 options.ClientSecret = "MoYpnVChG8VeDiGCw4XYrLH0";
                  //options.UserInformationEndpoint= "https://www.googleapis.com/oauth2/v1/certs";
                  options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                 options.ClaimActions.Clear();
                 options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                 options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                 options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                 options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                 options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                 options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
             });
            return services;
        }
    }
}
