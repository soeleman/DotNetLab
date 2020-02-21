using Microsoft.Extensions.DependencyInjection;

using Microsoft.OpenApi.Models;

namespace Microsoft.AspNetCore.Builder
{
    partial class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGenCommon(
            this IServiceCollection services,
            string title,
            string version = "v1")
        {
            services.AddSwaggerGen(o => o.SwaggerDoc(version, new OpenApiInfo { Title = title }));
            return services;
        }
    }
}
