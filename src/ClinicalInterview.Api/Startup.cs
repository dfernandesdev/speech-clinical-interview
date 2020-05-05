using ClinicalInterview.Api.Extensions;
using ClinicalInterview.Api.Templates;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicalInterview.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _config = env.GetMyHostConfiguration();
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyCors();
            services.AddMyRequestLocalizationOptions();
            services.AddMyControllers();
            services.AddMySwaggerGen();

            services.AddSingleton<IConverter>(x => new SynchronizedConverter(new PdfTools()));
            services.AddScoped<PatientIntakeFormTemplate>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMyExceptionHandler(_env);
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseRequestLocalization();
            app.UseMyEndpoints();
            app.UseMySwagger();
        }
    }
}
