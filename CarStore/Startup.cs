using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CarStore
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
            services.AddDbContext<CarsDbContext>(options => options.UseSqlServer(Configuration["Data:CarStore:ConnectionString"]));
            services.AddDbContext<CarStoreUserIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:CarStoreUserIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<CarStoreUserIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddScoped(s => SessionCart.GetCart(s));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMemoryCache();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{Page:int}",
                    defaults: new {controller = "Car", action = "List"}
                    );
                routes.MapRoute(
                    name: null,
                    template: "Page{Page:int}",
                    defaults: new { controller = "Car", action = "List", Page = 1 }
                    );
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Car", action = "List", Page = 1 }
                    );
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Car", action = "List", Page = 1 }
                    );
                routes.MapRoute(
                     name: null,
                     template: "{controller}/{action}/{id?}"
                     );
            });

            CarsSeed.EnsurePopulated(app);
            UserIdentitySeedData.EnsurePopulated(app);
        }
    }
}
