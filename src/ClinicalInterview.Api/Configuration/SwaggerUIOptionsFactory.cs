using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace ClinicalInterview.Api.Configuration
{
    public static class SwaggerUIOptionsFactory
    {
        public static Action<SwaggerUIOptions> Create()
        {
            return options =>
            {
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Speech - API");
            };
        }
    }
}
