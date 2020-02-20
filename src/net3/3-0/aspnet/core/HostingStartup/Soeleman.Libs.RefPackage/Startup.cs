using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Soeleman.Libs.RefPackage;

[assembly: HostingStartup(typeof(Startup))]

namespace Soeleman.Libs.RefPackage
{
    public class Startup :
        IHostingStartup
    {
        public void Configure(
            IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var dict = new Dictionary<string, string>
                {
                    { "RefPackage", "Soeleman.Libs.RefPackage" }
                };

                config.AddInMemoryCollection(dict);
            });
        }
    }
}