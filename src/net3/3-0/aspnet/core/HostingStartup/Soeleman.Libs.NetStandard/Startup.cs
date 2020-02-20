using System.Collections.Generic;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using Soeleman.Libs.NetStandard;

[assembly: HostingStartup(typeof(Startup))]

namespace Soeleman.Libs.NetStandard
{
    public class Startup :
        IHostingStartup
    {
        public void Configure(
            IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((webHostBuilderContext, config) =>
            {
                //var env = webHostBuilderContext.HostingEnvironment.ApplicationName;

                var env = "XXX";
#if DEBUG 
                env = "DEBUG";
#endif
                var dict = new Dictionary<string, string>
                {
                    { "NetStandard", "Soeleman.Libs.NetStandard" },
                    { "NetStandardEnvironmentName", env }
                };

                config.AddInMemoryCollection(dict);
            });
        }
    }
}