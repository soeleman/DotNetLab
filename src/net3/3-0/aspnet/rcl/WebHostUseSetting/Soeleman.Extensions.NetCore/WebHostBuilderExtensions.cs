using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Soeleman.Extensions.NetCore
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseApplication(
            this IWebHostBuilder hostBuilder,
            string entryAssemblyName,
            string contentPath)
        {
            return hostBuilder
                .ConfigureServices(services =>
                {
                    services.AddControllersWithViews();
                    services.AddRazorPages();
                })
                .Configure(app =>
                {
                     app.UseDeveloperExceptionPage();

                     app.UseStaticFiles();

                     app.UseRouting();

                     app.UseEndpoints(endpoints => 
                     {
                         endpoints.MapDefaultControllerRoute();
                         endpoints.MapRazorPages();
                     });
                 })
                .UseContentRoot(contentPath)
                .UseSetting(
                    WebHostDefaults.ApplicationKey, 
                    entryAssemblyName);
        }
    }
}