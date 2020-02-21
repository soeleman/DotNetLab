using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Builder
{
    public sealed class EfCoreStorageDbContext :
        DbContext
    {
        private readonly IConfiguration configuration;

        public EfCoreStorageDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }

            var efTypeConfig = configuration.BindGenericEfCoreConfiguration();

            if (efTypeConfig.DbSetTypes.Count == 0)
            {
                return;
            }

            efTypeConfig.DbSetTypes
                .Distinct()
                .Select(Type.GetType)
                .Where(p => p?.IsClass == true)
                .ToList()
                .ForEach(a =>
                {
                    if(a == null)
                    {
                        return;
                    }

                    var method = modelBuilder
                        .GetType()
                        .GetMethod("Entity", Array.Empty<Type>());

                    if (method == null)
                    {
                        return;
                    }

                    method = method.MakeGenericMethod(a);
                    method.Invoke(modelBuilder, null);
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}