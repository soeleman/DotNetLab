using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Soeleman.Web.Pages
{
    public class IndexModel : 
        PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration _configuration;

        public IEnumerable<string> Assemblies {get;}

        public IEnumerable<string> ConfigurationValues {get;}

        public IndexModel(
            ILogger<IndexModel> logger,
            IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
            
            Assemblies = Program
                .StartupAssemblies
                .Select(s => s.Split('.')[2])
                .ToList();

            ConfigurationValues = Assemblies
                .Select(s => config[s])
                .ToList();
        }

        public void OnGet()
        {
            ViewData["Config"] = "";
        }
    }
}
