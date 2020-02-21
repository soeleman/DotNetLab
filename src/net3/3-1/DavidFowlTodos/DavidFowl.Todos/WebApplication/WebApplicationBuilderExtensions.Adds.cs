using System;

namespace Microsoft.AspNetCore.Builder
{
    partial class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder Adds(
            this WebApplicationBuilder webApplicationBuilder,
            Action<WebApplicationBuilder>? option = null)
        {
            option?.Invoke(webApplicationBuilder);
            return webApplicationBuilder;
        }
    }
}
