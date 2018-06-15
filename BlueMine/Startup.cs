using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

            services.AddSingleton<ImageHandler, SixLaborsImageHandler>();
            services.AddTransient<Services.JsonSerializer, Services.NewtonJsonSerializer>();
            
            services.AddMvc();
            
            
            services.Configure<Microsoft.AspNetCore.Routing.RouteOptions>(options =>
                
                options.ConstraintMap.Add("BlueMineTable"
                    //, typeof(RouteConstraints.EntitiesRouteConstraint<BlueMine.Db.BlueMineContext>)
                    ,typeof(RouteConstraints.EntitiesRouteConstraint
                        <BlueMine.Db.BlueMineContext, 
                            BlueMine.Db.GenericEntityFramworkRepository<BlueMine.Db.BlueMineContext>,
                            BlueMine.Db.BlueMineRepository>)
                    )
            );
            
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
            
            // https://stackoverflow.com/questions/47997120/difference-between-map-and-mapwhen-branch-in-asp-net-core-middleware
            app.Map("/script.ashx", (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    context.Response.StatusCode = 200;
                    context.Response.Headers["Content-Type"] = "application/javascript; charset=utf-8";
                    string a = context.Request.Query["Single"];
                    a = System.IO.Path.Combine(env.WebRootPath, a);
                    
                    string text = "";
                    if(System.IO.File.Exists(a))
                        text = System.IO.File.ReadAllText(a, System.Text.Encoding.UTF8);
                    await context.Response.WriteAsync(text);
                });
            });
            
            
            /*
            // https://stackoverflow.com/questions/47997120/difference-between-map-and-mapwhen-branch-in-asp-net-core-middleware
            app.Map("SomePathMatch", (appBuilder) =>
                {
                    appBuilder.Run(async (context) => {

                        await context.Response.WriteAsync("");
                    });
                });
            // is equivalent to the following MapWhen call:

            app.MapWhen(context => context.Request.Path.StartsWithSegments("SomePathMatch"), (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    await context.Response.WriteAsync("");
                });
            });
            */
            
            /*
            app.MapWhen(context =>
            {
                var path = context.Request.Path.Value;
                return path.StartsWith("/assets", StringComparison.OrdinalIgnoreCase) ||
                       path.StartsWith("/lib", StringComparison.OrdinalIgnoreCase) ||
                       path.StartsWith("/app", StringComparison.OrdinalIgnoreCase);
            }, config => config.UseStaticFiles());
            */
            
            
            /*
            app.MapWhen(context => context.Request.Query.ContainsKey(""), (appBuilder) =>
            {
                appBuilder.Run(async (context) =>
                {
                    await context.Response.WriteAsync("hello world");
                });
                
            });
            */
            
            
            app.UseMvc();
            
            app.UseDefaultFiles( new DefaultFilesOptions()
            {
                DefaultFileNames   = new List<string>()
                {
                    "index.htm", "index.html", "slick.htm"
                }
            });
            
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
