using System;

using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    partial class GenericApiConfigurationExtensions
    {
        public static GenericApiEfCoreConfiguration BindGenericEfCoreConfiguration(
            this IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var genericEfCoreConfig = new GenericApiEfCoreConfiguration();
            configuration.GetSection(EfCoreDbSetConfig).Bind(genericEfCoreConfig);
            return genericEfCoreConfig;
        }
    }
}