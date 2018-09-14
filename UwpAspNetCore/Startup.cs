using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using UwpAspNetLib;
using UwpAspNetLib.Hubs;

namespace UwpAspNetCore
{
    class UwpStartup : Startup
    {
        public UwpStartup(IConfiguration configuration)
            :base(configuration)
        {
        }
        
        public override void ConfigureServices(IServiceCollection services)
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

            base.ConfigureServices(services);
        }
    }
}