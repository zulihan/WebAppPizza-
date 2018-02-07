using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppPizza.Data;
using WebAppPizza.Models;
using WebAppPizza.Services;
using Microsoft.AspNetCore.Http;

namespace WebAppPizza
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<PizzaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PizzaConnection")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.  transiant// scope// singleton//////   a la demande partager// pour le meme pool de l'application //  unique pour l'aplication
            services.AddTransient<IRepositoryable<Pizza>, PizzaRepository>();  //  quand on peut pas faire du singleton on fait  du transiant ou scope
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<IStaticRepository, StaticRepository>();
            services.AddScoped<Cart>(Sp => SessionCart.GetCart(Sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");

            });
            await SeedUserAndRole.EnsurePopulateRole(app); // appelle les methode pour ajouter des comptes dans ta base
            await SeedUserAndRole.EnsurePopulateUserAdmin(app);// erreur car le provider default , il sait pas si cest en transiant/scope/singleton donc on rajoute dans la classe programme comment ils fonctionne
        }
    }
}
