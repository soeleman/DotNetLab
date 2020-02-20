using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Soeleman.Libs.RefStore;

[assembly: HostingStartup(typeof(Startup))]

namespace Soeleman.Libs.RefStore
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
                    { "RefStore", "Soeleman.Libs.RefStore" }
                };

                config.AddInMemoryCollection(dict);
            });
        }
    }
}