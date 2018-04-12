using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            // Do some reflection to get at the internal stuff we need to override
            var assembliesProviderType = Assembly.Load("Microsoft.AspNetCore.Mvc.Core").GetType("Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationAssembliesProvider");
            var resolveAssembliesMethod = assembliesProviderType.GetMethod("ResolveAssemblies");

            // Now implement enough of the internal functionality for things to pass
            var assembliesProvider = Activator.CreateInstance(assembliesProviderType);
            var applicationAssemblies = (IEnumerable<Assembly>)resolveAssembliesMethod.Invoke(assembliesProvider, new object[] { Assembly.GetExecutingAssembly() });

            foreach (var assembly in applicationAssemblies)
            {
                // TODO: Find a better way to do this
                manager.ApplicationParts.Add(new AssemblyPart(Assembly.Load("UwpAspNetLib")));

                var partFactory = ApplicationPartFactory.GetApplicationPartFactory(assembly);
                foreach (var part in partFactory.GetApplicationParts(assembly))
                {
                    manager.ApplicationParts.Add(part);
                }
            }

            services.AddSingleton(manager);
            services.AddMvc(options =>
            {

            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //env.ApplicationName = "UwpAspNetLib";
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync($"Hello world! {DateTime.Now}");
            //});

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