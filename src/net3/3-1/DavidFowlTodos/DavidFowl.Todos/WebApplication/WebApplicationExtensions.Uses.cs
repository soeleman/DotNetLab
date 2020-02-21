using System;

namespace Microsoft.AspNetCore.Builder
{
    partial class WebApplicationExtensions
    {
        public static WebApplication Uses(
            this WebApplication webApplication,
            Action<WebApplication>? option = null)
        {
            if (option == null)
            {
                webApplication.MapControllers();
                return webApplication;
            }

            option.Invoke(webApplication);
            return webApplication;
        }
    }
}
