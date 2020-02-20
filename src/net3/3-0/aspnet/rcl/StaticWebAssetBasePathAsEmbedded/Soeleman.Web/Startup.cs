using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Soeleman.Libs.SayHi;

namespace Soeleman.Web
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddSoelemanLibsSayHi();
            services.AddRazorPages();
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //var dllPath = Path.Join(Path.GetDirectoryName(path: Assembly.GetEntryAssembly()?.Location), "Soeleman.Libs.SayHi.dll");
            //var embeddedProvider = new ManifestEmbeddedFileProvider(Assembly.LoadFrom(dllPath),"wwwroot");

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = embeddedProvider
            //});

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}