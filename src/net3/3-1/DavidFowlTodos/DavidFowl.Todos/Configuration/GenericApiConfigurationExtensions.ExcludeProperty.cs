using System;

using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    partial class GenericApiConfigurationExtensions
    {
        public static string[] ExcludeProperty(
            this IConfiguration configuration,
            Type controller,
            Type typeController)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (typeController == null)
            {
                throw new ArgumentNullException(nameof(typeController));
            }

            var sectionController = $"{GenericApiConfig}:Controllers:{controller.Namespace}.{controller.Name}:Types";

            foreach (var section in configuration
                .GetSection(sectionController)
                .GetChildren())
            {
                var typeControllerAssemblyQualifiedName = configuration.GetSection($"{sectionController}:{section.Key}:Type").Value;

                if (!typeControllerAssemblyQualifiedName.Equals(
                    typeController.AssemblyQualifiedName,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                return configuration.GetSection($"{sectionController}:{section.Key}:Exclude").Value.Split(',');
            }

            return Array.Empty<string>();
        }
    }
}
