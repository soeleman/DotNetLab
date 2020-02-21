using System;

using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    partial class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder BuildServices(
            this WebApplicationBuilder webApplicationBuilder,
            Action<WebApplicationBuilder, IServiceCollection>? option = null)
        {
            option?.Invoke(webApplicationBuilder, webApplicationBuilder.Services);
            return webApplicationBuilder;
        }
    }
}
