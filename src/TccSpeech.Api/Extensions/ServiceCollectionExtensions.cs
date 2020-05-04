using AdesaoIndividualQueries.Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AdesaoIndividualQueries.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyCors(this IServiceCollection services) =>
            services.AddCors(CorsOptionsFactory.Create());

        public static IServiceCollection AddMyRequestLocalizationOptions(this IServiceCollection services) =>
            services.Configure(RequestLocalizationOptionsFactory.Create());

        public static IServiceCollection AddMyControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(MvcNewtonsoftJsonOptionsFactory.Create());
            return services;
        }

        public static IServiceCollection AddMySwaggerGen(this IServiceCollection services) =>
            services.AddSwaggerGen(SwaggerGenOptionsFactory.Create());
    }
}
