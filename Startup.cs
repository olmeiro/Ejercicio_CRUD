using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDSinInyeccionASP.Models.Abstract;
using CRUDSinInyeccionASP.Models.Business;
using CRUDSinInyeccionASP.Models.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CRUDSinInyeccionASP
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
            services.AddControllersWithViews();

            var conexion = Configuration["ConnectionStrings:conexion_sqlServer"];

            services.AddDbContext<DbContextPrueba>(options =>
            options.UseSqlServer(conexion));

            services.AddScoped<IEmpleadoBusiness, EmpleadoBusiness>();
            services.AddScoped<ICargoBusiness, CargoBusiness>();
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                     //pattern: "{controller=Empleados}/{action=Index}/{id?}");
                     //pattern: "{controller=CargoEmpleado}/{action=Index}/{id?}");
            });
        }
    }
}
