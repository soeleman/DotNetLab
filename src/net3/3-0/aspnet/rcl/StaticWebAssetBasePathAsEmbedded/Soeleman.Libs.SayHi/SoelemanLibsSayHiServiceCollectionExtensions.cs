using Microsoft.Extensions.DependencyInjection;

namespace Soeleman.Libs.SayHi
{
    public static class SoelemanLibsSayHiServiceCollectionExtensions
    {
        public static void AddSoelemanLibsSayHi(
            this IServiceCollection services)
        {
            services.ConfigureOptions(typeof(SayHiConfigureOptions));
        }
    }
}