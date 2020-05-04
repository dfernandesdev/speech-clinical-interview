using AdesaoIndividualQueries.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AdesaoIndividualQueries.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMyExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            if (env.IsStaging() || env.IsProduction())
                app.UseExceptionHandler(ExceptionHandlerOptionsFactory.Create());
            return app;
        }

        public static IApplicationBuilder UseMyEndpoints(this IApplicationBuilder app) =>
            app.UseEndpoints(endpoints => endpoints.MapControllers());

        public static IApplicationBuilder UseMySwagger(this IApplicationBuilder app) =>
            app.UseSwagger().UseSwaggerUI(SwaggerUIOptionsFactory.Create());
    }
}
