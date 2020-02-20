using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Soeleman.Libs.SayHi;

[assembly: HostingStartup(typeof(Startup))]

namespace Soeleman.Libs.SayHi
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
                    { "SayHi", "Soeleman.Libs.SayHi" }
                };

                config.AddInMemoryCollection(dict);
            });
        }
    }
}