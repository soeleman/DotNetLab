using System;

using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    partial class GenericApiConfigurationExtensions
    {
        public static GenericApiConfiguration BindGenericApiConfiguration(
            this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var genericApiConfig = new GenericApiConfiguration();
            configuration.GetSection(GenericApiConfig).Bind(genericApiConfig);
            return genericApiConfig;
        }
    }
}
