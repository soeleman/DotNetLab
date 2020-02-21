using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Todos
{
    public static class Program
    {
        public static async Task Main(
            string[] args) =>
            await WebApplication
                .CreateBuilder(args)
                .BuildConfigurations(o => o.AddInMemoryCollection(o.KeyValueDbSetAndGenericApiBuilder(typeof(GenericApiController<,>), (typeof(Todo), typeof(int), nameof(Todo.Id)))))
                .BuildServices((b, o) => o
                    .AddDbContext<EfCoreStorageDbContext>(options => options.UseInMemoryDatabase("Todos"))
                    .AddScoped(typeof(IStorage<,>), typeof(EfCoreStorage<,>))
                    .AddSwaggerGenCommon("Todo API")
                    .AddControllers(o => o.Conventions.Add(new GenericApiNameConvention()))
                    .ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericApiFeature(b.Configuration))))
                .Build()
                .Uses(o => o.UseSwaggerCommon().MapControllers())
                .RunAsync()
                .ConfigureAwait(false);
    }
}
