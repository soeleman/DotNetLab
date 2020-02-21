using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    partial class GenericApiConfigurationExtensions
    {
        private const string EfCoreDbSetConfig = "EfCoreDbSet";

        public static IEnumerable<KeyValuePair<string, string>> KeyValueDbSet(
            this IConfiguration configuration,
            params Type[] typeArgs)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return KeyValueDbSet(typeArgs);
        }

        public static IEnumerable<KeyValuePair<string, string>> KeyValueDbSetBuilder(
            this IConfigurationBuilder configuration,
            params Type[] typeArgs)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return KeyValueDbSet(typeArgs);
        }

        private static IEnumerable<KeyValuePair<string, string>> KeyValueDbSet(
            params Type[] typeArgs)
        {
            return typeArgs
                .Select((s, i) =>
                    new KeyValuePair<string, string>(
                        $"{EfCoreDbSetConfig}:DbSetTypes:{i}",
                        string.IsNullOrEmpty(s.AssemblyQualifiedName)
                            ? string.Empty
                            : s.AssemblyQualifiedName))
                .Where(p => !string.IsNullOrEmpty(p.Value));
        }
    }
}
