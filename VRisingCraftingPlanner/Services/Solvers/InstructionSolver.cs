using VRisingCraftingPlanner.Instructions;
using VRisingCraftingPlanner.Models;
using VRisingCraftingPlanner.Services.Stores;

namespace VRisingCraftingPlanner.Services.Solvers;

public sealed class InstructionSolver(InventoryStore inventoryStore, RecipeStore recipeStore, ItemBalanceStore itemBalanceStore)
{
  /// <summary>
  /// Receives the items the player would like to acquire, and the items they already have, and provides a series of
  /// instructions for acquiring those items.
  /// </summary>
  /// <returns>A series of tasks the player should undertake to acquire the items.</returns>
  public void Solve()
  {
    var instructions = new List<IInstruction>();
    
    //Add items to inventory
    itemBalanceStore.Add(inventoryStore.GetInventory());

    //Craft everything we need
    foreach (var item in itemBalanceStore.GetAllItems().ToList())
    {
      if (item.Count < 1)
      {
        var recipe = recipeStore.GetRecipeForItem(item.ItemType);
        var productCount = recipe.Products.First(x => x.ItemType == item.ItemType).Count;
        var craftsNeeded = item.Count / productCount * -1;
        for (var i = 0; i < craftsNeeded; i++)
        {
          itemBalanceStore.Add(recipe.Products);
          itemBalanceStore.Subtract(recipe.Ingredients);
        }
        instructions.Add(new CraftInstruction(recipe, craftsNeeded));
      }
    }

    foreach (var instruction in instructions)
    {
      Console.WriteLine(instruction.Message);
    }
  }
}