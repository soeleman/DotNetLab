using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    partial class GenericApiConfigurationExtensions
    {
        public static IEnumerable<KeyValuePair<string, string>> KeyValueDbSetAndGenericApiBuilder(
            this IConfigurationBuilder configuration,
            Type controller,
            params (Type api, Type identifier, string exclude)[] typeArgs)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var result = new List<KeyValuePair<string, string>>();
            result.AddRange(KeyValueDbSet(typeArgs.Select(s => s.api).ToArray()));
            result.AddRange(KeyValueGenericApi(controller, typeArgs));

            return result;
        }

        public static IEnumerable<KeyValuePair<string, string>> KeyValueDbSetAndGenericApi(
            this IConfiguration configuration,
            Type controller,
            params (Type api, Type identifier, string exclude)[] typeArgs)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var result = new List<KeyValuePair<string, string>>();
            result.AddRange(KeyValueDbSet(typeArgs.Select(s => s.api).ToArray()));
            result.AddRange(KeyValueGenericApi(controller, typeArgs));

            return result;
        }
    }
}