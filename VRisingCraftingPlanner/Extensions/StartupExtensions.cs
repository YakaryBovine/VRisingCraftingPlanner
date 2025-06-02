using Microsoft.Extensions.DependencyInjection;
using VRisingCraftingPlanner.Services.Readers;
using VRisingCraftingPlanner.Services.Solvers;
using VRisingCraftingPlanner.Services.Stores;

namespace VRisingCraftingPlanner.Extensions;

public static  class StartupExtensions
{
  public static IServiceCollection AddCraftingPlannerServices(this IServiceCollection services)
  {
    return services
      .AddTransient<RecipeStore>()
      .AddTransient<InstructionSolver>()
      .AddSingleton<ItemBalanceStore>()
      .AddSingleton<ItemTypeStore>()
      .AddTransient<RecipesReader>()
      .AddTransient<ItemTypesReader>()
      .AddScoped<InventoryStore>()
      .AddTransient<InventoryReader>()
      .AddSingleton<InstructionStore>();
  }
}