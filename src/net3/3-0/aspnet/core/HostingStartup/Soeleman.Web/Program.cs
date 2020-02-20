using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Soeleman.Web
{
    public static class Program
    {
        internal static string[] StartupAssemblies = new[]
        {
            "Soeleman.Libs.SayHi",
            "Soeleman.Libs.RefStore",
            "Soeleman.Libs.RefProject",
            "Soeleman.Libs.RefPackage",
            "Soeleman.Libs.Original",
            "Soeleman.Libs.NetStandard"
        };


        public static void Main(
            string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(
            string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSetting(
                        WebHostDefaults.HostingStartupAssembliesKey, 
                        string.Join(";", StartupAssemblies));

                    webBuilder.UseStartup<Startup>();
                });
    }
}

// http://www.programmersought.com/article/675755583/
// https://weekly-geekly.github.io/articles/327686/index.html
// https://cetus.io/tim/ASPNET-Core-2.0-Stripping-Away-Cross-Cutting-Concerns/
// http://beckjin.com/2018/10/28/skywalking-netcore-noinvade/
// http://mypublicnotepad.com/2018/03/31/dynamic-assembly-loading-in-aspnet-core/
