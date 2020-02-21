namespace Microsoft.AspNetCore.Builder
{
    partial class SwaggerExtensions
    {
        public static IApplicationBuilder UseSwaggerCommon(
            this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API");
                o.RoutePrefix = string.Empty;
            });

            return app;
        }

        public static WebApplication UseSwaggerCommon(
            this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API");
                o.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
