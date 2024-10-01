using Infrastructure.Data;
using Serilog;

namespace ScaffoldingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }
            
        private static void RunSeeding(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<SeedDb>();
                seeder.ExecSeedAsync().Wait();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseSerilog((hostContext, services, configuration) =>
            {
                configuration.WriteTo.Console();
                configuration.MinimumLevel.Error();
                configuration.WriteTo.File("Logs/Scaffolding.txt", rollingInterval: RollingInterval.Day);

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
