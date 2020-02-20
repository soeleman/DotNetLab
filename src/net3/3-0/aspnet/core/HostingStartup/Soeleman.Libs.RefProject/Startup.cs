using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Soeleman.Libs.RefProject;

[assembly: HostingStartup(typeof(Startup))]

namespace Soeleman.Libs.RefProject
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
                    { "RefProject", "Soeleman.Libs.RefProject" }
                };

                config.AddInMemoryCollection(dict);
            });
        }
    }
}