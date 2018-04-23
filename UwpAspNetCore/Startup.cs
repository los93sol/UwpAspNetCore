using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UwpAspNetLib;

namespace UwpAspNetCore
{
    class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var manager = new ApplicationPartManager();
            var assemblies = new Assembly[] { Assembly.GetExecutingAssembly(), Assembly.Load("UwpAspNetLib"), Assembly.Load("UwpAspNetLib.Views") };

            foreach (var assembly in assemblies)
            {
                var factory = ApplicationPartFactory.GetApplicationPartFactory(assembly);

                foreach (var part in factory.GetApplicationParts(assembly))
                {
                    manager.ApplicationParts.Add(part);
                }
            }

            services.AddSingleton(manager);
            services.AddMvc(options =>
            {

            });

            manager.FeatureProviders.Add(new UWPViewsFeatureProvider());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}