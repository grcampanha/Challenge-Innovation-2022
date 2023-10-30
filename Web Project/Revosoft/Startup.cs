using Revosoft.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Revosoft.Repositories;
using Revosoft.Repositories.Interfaces;
using Revosoft.Models;
using ReflectionIT.Mvc.Paging;

namespace WebApplication1
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
            services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

            services.AddControllersWithViews();

            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageindex";
            });

            services.AddAuthentication("Identity.Login")
                .AddCookie("Identity.Login", config =>
                {
                    config.Cookie.Name = "Identity.Login";
                    config.LoginPath = "/Home";
                    config.AccessDeniedPath = "/Home";
                    config.ExpireTimeSpan = TimeSpan.FromHours(1);
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    politica =>
                    {
                        politica.RequireRole("Admin");
                    });
            });

            services.AddMemoryCache();
            services.AddSession();
            services.AddRazorPages();
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

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}