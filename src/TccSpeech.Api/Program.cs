using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AdesaoIndividualQueries.Api
{
    public static class Program
    {
        public static void Main(string[] args) =>
            CreateHost(args).Run();

        public static IHost CreateHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).Build();
    }
}
