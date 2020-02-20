using System.IO;
using System.Reflection;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Soeleman.Extensions.NetCore;

namespace Soeleman.Web
{
    public static class Program
    {
        public static void Main(
            string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(
            string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseApplication(
                    Assembly.GetExecutingAssembly().GetName().Name, 
                    Directory.GetCurrentDirectory());
                
    }
}