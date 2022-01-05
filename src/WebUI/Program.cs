using CaWorkshop.Infrastructure.Data;

namespace CaWorkshop.WebUI;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            try
            {

                var initialiser = scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContextInitialiser>();

                initialiser.Update();
                initialiser.Seed();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider
                    .GetRequiredService<ILogger<Program>>();

                logger.LogError(ex,
                    "An error occurred during database initialisation.");

                throw;
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
