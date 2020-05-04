using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace ClinicalInterview.Api.Configuration
{
    public static class MvcNewtonsoftJsonOptionsFactory
    {
        public static Action<MvcNewtonsoftJsonOptions> Create()
        {
            return options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            };
        }
    }
}
