using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VRisingCraftingPlanner.Data;
using VRisingCraftingPlanner.Extensions;
using VRisingCraftingPlanner.Services;

namespace VRisingCraftingPlanner
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      using var host = CreateHostBuilder(args).Build();
      
      var solver = host.Services.GetRequiredService<InstructionSolver>();
      solver.Solve([], [
        new Item
        {
          ItemType = new ItemType("Iron Ingot"),
          Count = 5
        }
      ]);

      await host.StopAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .ConfigureServices((_, services) =>
        {
          services
            .AddLogging(config => { config.AddConsole(); })
            .AddRecipes()
            .AddCraftingPlannerServices();
        });
    }
  }
}