using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitio.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sitio
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

            //Agrego configuracion de Session------------------------------------------------------------
            //Agrego desde aqui -------------------------------------------------------------------------
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(600); //Tenia 10, aca se determina cuanto tiempo duran los datos en la Session
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //Hasta aqui --------------------------------------------------------------------------------




            //Agrego configuracion de IHttpContextAccessor  (Para Session) ------------------------------
            //Agrego desde aqui -------------------------------------------------------------------------
            services.AddMvc();
            services.AddHttpContextAccessor();
            //Agrego desde aqui -------------------------------------------------------------------------



            //Agrego configuracion SignalR --------------------------------------------------------------
            //Agrego desde aqui -------------------------------------------------------------------------
            services.AddSignalR();
            //Agrego desde aqui -------------------------------------------------------------------------



            services.AddSingleton<MonitoreoRed>(); //Clase en la que esta implementada Singleton
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

            app.UseAuthorization();


            //Agrego configuracion de Session------------------------------------------------------------
            //Agrego desde aqui -------------------------------------------------------------------------
            app.UseSession();
            //Hasta aqui --------------------------------------------------------------------------------


            //Agrego configuracion SignalR --------------------------------------------------------------
            //Agrego desde aqui -------------------------------------------------------------------------

            //Antes era asi
            //app.UseSignalR(routes => { routes.MapHub<MonitoreoRedHub>("/monitoreoRedHub") });

            //Ahora es asi...
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapHub<MonitoreoRedHub>("/monitoreoRedHub");
            //});

            //Agrego desde aqui -------------------------------------------------------------------------


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Logueo}/{id?}"); //Revisar, creo que hay que sacar ID


                //Indico clase Hub de SignalR, lo movi para aqui para optimizar
                endpoints.MapHub<MonitoreoRedHub>("/monitoreoRedHub");
            });
        }
    }
}
