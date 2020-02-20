using Microsoft.AspNetCore.Components.Builder;

namespace Soeleman.Wasm.App.Client
{
    public class Startup
    {
        public void Configure(
            IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
