using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace BlueMine
{
    
    
    public class Startup
    {
        
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.

        // https://stackoverflow.com/questions/38138100/what-is-the-difference-between-services-addtransient-service-addscope-and-servi

        // Singleton which creates a single instance throughout the application.
        // It creates the instance for the first time and reuses the same object in the all calls.

        // Scoped lifetime services are created once per request within the scope.
        // It is equivalent to Singleton in the current scope. 
        // eg. in MVC it creates 1 instance per each http request 
        // but uses the same instance in the other calls within the same web request.

        // Transient lifetime services are created each time they are requested.
        // This lifetime works best for lightweight, stateless services.

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor,
                    Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services
                .AddSingleton<Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor,
                    Microsoft.AspNetCore.Mvc.Infrastructure.ActionContextAccessor>();

            // services.AddDbContext<Db.BlueMineContext>(ServiceLifetime.Scoped);

            services.AddDbContext<BlueMine.Db.BlueMineContext>(
                delegate(DbContextOptionsBuilder options)
                {
                    // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                    // options.UseNpgsql("");
                    // options.UseSqlite("");

                    options.UseSqlServer(BlueMine.Data.SqlFactory.ConnectionString);
                }
            );

            services.AddScoped<BlueMine.Db.BlueMineRepository>();

            services.AddMvc();
        } // End Sub ConfigureServices 


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            string virtualDirectory = "/Virt_DIR";
            virtualDirectory = "/";
            
            if (virtualDirectory.EndsWith("/"))
                virtualDirectory = virtualDirectory.Substring(0, virtualDirectory.Length - 1);
            
            if (string.IsNullOrWhiteSpace(virtualDirectory))
                ConfigureMapped(app, env); // Don't map if you don't have to 
            // (wonder what the framework does or does not  do for that case)
            else
                app.Map(virtualDirectory,
                    delegate(IApplicationBuilder mappedApp) { ConfigureMapped(mappedApp, env); }
                );
        } // End Sub Configure 
        
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void ConfigureMapped(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseStaticFiles();
            
            /*
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            */
            
        } // End Sub ConfigureMapped 
        
        
    } // End Class Startup 
    
    
} // End Namespace BlueMine 