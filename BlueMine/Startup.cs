
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddSingleton<Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor, Microsoft.AspNetCore.Mvc.Infrastructure.ActionContextAccessor>();

            // services.AddDbContext<Redmine.RedmineContext>(ServiceLifetime.Scoped);

            services.AddDbContext<Redmine.RedmineContext>(
                delegate(DbContextOptionsBuilder options)
            { 
                // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                // options.UseNpgsql("");
                // options.UseSqlite("");
                
                options.UseSqlServer(SqlFactory.ConnectionString);
            });
            
            services.AddMvc();
        } // End Sub ConfigureServices 


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            string virtual_directory = "/Virt_DIR";
            virtual_directory = "/";

            if (virtual_directory.EndsWith("/"))
                virtual_directory = virtual_directory.Substring(0, virtual_directory.Length - 1);

            if (string.IsNullOrWhiteSpace(virtual_directory))
                ConfigureMapped(app, env); // Don't map if you don't have to 
                                                     // (wonder what the framework does or does not  do for that case)
            else
                app.Map(virtual_directory, 
                    delegate (IApplicationBuilder mappedApp)
                    {
                        ConfigureMapped(mappedApp, env);
                    }
                );
        }


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
            
        }
        
        
    } // End Class Startup 
    
    
} // End Namespace BlueMine 
