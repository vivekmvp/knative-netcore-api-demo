using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace TodoApiLatest.Extensions
{
    public static class SwaggerExtension
    {
        public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
        {
            IServiceProvider services = app.ApplicationServices;
            var provider = services.GetRequiredService<IApiVersionDescriptionProvider>();
            
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {                       
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToLowerInvariant());
                }
            });

            return app;
        }
    }
}
