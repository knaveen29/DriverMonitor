using Microsoft.AspNetCore;

namespace DriverMonitorBackend
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ApiDBContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    //context.Database.EnsureCreated();
                    //DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            host.Run();
            return 1;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseUrls("http://0.0.0.0:5000")
             .ConfigureLogging((hostingContext, logging) =>
             {
                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                 logging.AddConsole();
                 logging.AddDebug();
             })
            .UseStartup<Startup>();
    }
}