using Microsoft.Extensions.DependencyInjection;
using VRisingCraftingPlanner.Services;

namespace VRisingCraftingPlanner.Extensions;

public static  class StartupExtensions
{
  public static IServiceCollection AddCraftingPlannerServices(this IServiceCollection services)
  {
    return services
      .AddTransient<RecipeBook>()
      .AddTransient<InstructionSolver>()
      .AddSingleton<InventoryManager>();
  }
}