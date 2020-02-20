using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;

namespace Soeleman.Wasm.Server
{
    public static class BlazorExtensions
    {
        private const string BlazorConfig   = ".blazor.config";
        private const string FolderWwwwRoot = "wwwroot";
        private const string FolderDist     = "dist";

        public static IApplicationBuilder UseClientSideBlazorFiles<T>(
            this IApplicationBuilder app,
            Dictionary<string, string> mimes)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof (app));
            }

            return UseClientSideBlazorFiles(
                app, 
                typeof(T).Assembly.Location, 
                mimes);
        }

        public static IApplicationBuilder UseClientSideBlazorFiles(
            this IApplicationBuilder app, 
            string assemblyPath,
            Dictionary<string, string> mimes)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof (app));
            }

            var webRootPath = string.Empty;

            if (string.IsNullOrEmpty(assemblyPath))
            {
                throw new ArgumentNullException(nameof (assemblyPath));
            }

            app.UseClientSideBlazorFiles(assemblyPath);

            var configList = File
                .ReadLines(Path.ChangeExtension(assemblyPath, BlazorConfig))
                .ToList();

            var sourceMsBuildPath = configList[0];

            if (sourceMsBuildPath == ".")
            {
                sourceMsBuildPath = assemblyPath;
            }

            var directoryName = Path.GetDirectoryName(sourceMsBuildPath);

            var path = Path.Combine(
                directoryName, 
                FolderWwwwRoot);

            if (Directory.Exists(path))
            {
                webRootPath = path;
            }

            var distPath = Path.Combine(
                Path.GetDirectoryName(Path.Combine(directoryName, configList[1])),
                FolderDist);

            var fileProviderList = new List<IFileProvider>
            {
                new PhysicalFileProvider(distPath)
            };

            if (!string.IsNullOrEmpty(webRootPath))
            {
                fileProviderList.Add(new PhysicalFileProvider(webRootPath));
            }

            var provider = new FileExtensionContentTypeProvider();

            if (mimes.Count >= 1)
            {
                foreach (var (key, value) in mimes)
                {
                    AddMapping(provider, key, value);
                }
            }
            
            var staticFileOptions = new StaticFileOptions
            {
                ContentTypeProvider = provider,
                FileProvider        = new CompositeFileProvider(fileProviderList),
                OnPrepareResponse   = SetCacheHeaders
            };

            app.UseStaticFiles(staticFileOptions);

            return app;
        }

        private static void AddMapping(
            FileExtensionContentTypeProvider provider,
            string name, 
            string mimeType)
        {
            if (provider.Mappings.ContainsKey(name))
            {
                return;
            }

            provider.Mappings.Add(name, mimeType);
        }

        private static void SetCacheHeaders(
            StaticFileResponseContext ctx)
        {
            var typedHeaders = ctx.Context.Response.GetTypedHeaders();

            if (typedHeaders.CacheControl != null)
            {
                return;
            }

            typedHeaders.CacheControl = new CacheControlHeaderValue()
            {
                NoCache = true
            };
        }
    }
}