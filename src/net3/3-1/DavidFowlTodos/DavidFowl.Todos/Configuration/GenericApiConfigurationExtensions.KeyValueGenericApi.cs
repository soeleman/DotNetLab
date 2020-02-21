using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    partial class GenericApiConfigurationExtensions
    {
        private const string GenericApiConfig = "GenericApi";

        public static IEnumerable<KeyValuePair<string, string>> KeyValueGenericApi(
            this IConfiguration configuration,
            Type controller,
            params (Type api, Type identifier, string exclude)[] typeArgs)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (controller == null ||
                string.IsNullOrEmpty(controller.AssemblyQualifiedName))
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }

            return KeyValueGenericApi(controller, typeArgs);
        }

        public static IEnumerable<KeyValuePair<string, string>> KeyValueGenericApiBuilder(
            this IConfigurationBuilder configuration,
            Type controller,
            params (Type api, Type identifier, string exclude)[] typeArgs)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (controller == null ||
                string.IsNullOrEmpty(controller.AssemblyQualifiedName))
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }

            return KeyValueGenericApi(controller, typeArgs);
        }

        private static IEnumerable<KeyValuePair<string, string>> KeyValueGenericApi(
            Type controller,
            params (Type api, Type identifier, string exclude)[] typeArgs)
        {
            if (controller == null ||
                string.IsNullOrEmpty(controller.AssemblyQualifiedName))
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }

            var result = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(
                    $"{GenericApiConfig}:Controllers:{controller.FullName}:AssemblyQualifiedName",
                    controller.AssemblyQualifiedName)
            };

            for (var i = 0; i < typeArgs.Length; i++)
            {
                var (api, identifier, exclude) = typeArgs[i];

                if (api == null ||
                    identifier == null ||
                    string.IsNullOrEmpty(api.AssemblyQualifiedName) ||
                    string.IsNullOrEmpty(identifier.AssemblyQualifiedName))
                {
                    continue;
                }

                result.AddRange(new[]
                {
                    new KeyValuePair<string, string>($"{GenericApiConfig}:Controllers:{controller.FullName}:Types:{i}:Type", api.AssemblyQualifiedName),
                    new KeyValuePair<string, string>($"{GenericApiConfig}:Controllers:{controller.FullName}:Types:{i}:IdType", identifier.AssemblyQualifiedName),
                    new KeyValuePair<string, string>($"{GenericApiConfig}:Controllers:{controller.FullName}:Types:{i}:Exclude", exclude)
                });
            }

            return result;
        }
    }
}
