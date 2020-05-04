using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ClinicalInterview.Api.Extensions
{
    public static class WebHostEnvironmentExtensions
    {
        public static IConfigurationRoot GetMyHostConfiguration(this IWebHostEnvironment env) =>
            new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
    }
}
