using System;

using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    partial class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder BuildConfigurations(
            this WebApplicationBuilder webApplicationBuilder,
            Action<IConfigurationBuilder>? option = null)
        {
            option?.Invoke(webApplicationBuilder.Configuration);
            return webApplicationBuilder;
        }
    }
}
