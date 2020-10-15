using System.Threading.Tasks;
using BoundfoxStudios.Computermuseum.WebApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BoundfoxStudios.Computermuseum.WebApi
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var scope = host.Services.CreateScope())
      {
        var serviceProvider = scope.ServiceProvider.GetRequiredService<DataInitializer>();

        var overrideExisting = false;

#if DEBUG
        overrideExisting = true;
#endif

        await serviceProvider.InitializeAsync(overrideExisting);
      }

      await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }
}
