using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Soeleman.Libs.Original;

[assembly: HostingStartup(typeof(Startup))]

namespace Soeleman.Libs.Original
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
                    { "Original", "Soeleman.Libs.Original" }
                };

                config.AddInMemoryCollection(dict);
            });
        }
    }
}