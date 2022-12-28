using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace lab2
{
    public class Program
    {
        
        private static IHostBuilder CreateSimulatorHostBuilder(LaunchServiceConfig config)
        {
            return Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
            {
                services.AddHostedService<PrincessSimulatorService>();
                services.AddScoped<PrincessSimulator>();
                services.AddScoped<Hall>();
                services.AddScoped<Friend>();
                services.AddScoped<ContenderGenerator>();
                services.AddScoped<ClassicalPrincessStrategy>();
                services.AddSingleton(config);
                services.AddDbContextFactory<DataContext>(options =>
                    options.UseNpgsql(
                        "server=localhost;" +
                                      "Port=5433;" +
                                      "Database=postgres;" +
                                      "User Id=postgres;" +
                                      "Password=csharp_password"));
            })
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                });
        }
        private static IHostBuilder CreatePrincessHostBuilder(LaunchServiceConfig config)
        {
            return Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
            {
                services.AddHostedService<Princess>();
                services.AddScoped<Hall>();
                services.AddScoped<Friend>();
                services.AddScoped<ContenderGenerator>();
                services.AddScoped<ClassicalPrincessStrategy>();
            });
        }

        private static LaunchServiceConfig CreateLaunchServiceConfig(string[] args)
        {
            return new LaunchServiceConfig {AttemptNames = args.ToList()};
        }

        public static void Main(string[] args)
        {
            var config = CreateLaunchServiceConfig(args);
            CreateSimulatorHostBuilder(config).Build().Run();
        }
    }
}