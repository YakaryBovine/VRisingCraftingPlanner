using Microsoft.Extensions.DependencyInjection;
using VRisingCraftingPlanner.Data;

namespace VRisingCraftingPlanner.Extensions;

public static class RecipeExtensions
{
  public static IServiceCollection AddRecipes(this IServiceCollection services)
  {
    services.AddSingleton(new Recipe([
      new Item
      {
        ItemType = new ItemType("Iron Ore", ItemOrigin.Ingredient),
        Count = 15
      }
    ], [
      new Item
      {
        ItemType = new ItemType("Iron Ingot", ItemOrigin.Product),
        Count = 1
      }
    ], "Furnace"));
    
    return services;
  }
}