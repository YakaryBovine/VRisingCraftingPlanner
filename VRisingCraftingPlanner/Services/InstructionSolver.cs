using VRisingCraftingPlanner.Data;

namespace VRisingCraftingPlanner.Services;

public sealed class InstructionSolver(RecipeBook recipeBook, InventoryManager inventoryManager)
{
  /// <summary>
  /// Receives the items the player would like to acquire, and the items they already have, and provides a series of
  /// instructions for acquiring those items.
  /// </summary>
  /// <param name="inventory">Items the player already has.</param>
  /// <param name="wishlist">Items the player would like to have.</param>
  /// <returns>A series of tasks the player should undertake to acquire the items.</returns>
  public List<IInstruction> Solve(List<Item> inventory, List<Item> wishlist)
  {
    var instructions = new List<IInstruction>();
    
    //Add items to inventory
    inventoryManager.Add(inventory);
    inventoryManager.Subtract(wishlist);

    //Craft everything we need
    foreach (var item in inventoryManager.GetAllItems().ToList())
    {
      if (item.Count < 1)
      {
        var recipe = recipeBook.GetRecipeForItem(item.ItemType);
        var productCount = recipe.Products.First(x => x.ItemType == item.ItemType).Count;
        var craftsNeeded = item.Count / productCount * -1;
        for (var i = 0; i < craftsNeeded; i++)
        {
          inventoryManager.Add(recipe.Products);
          inventoryManager.Subtract(recipe.Components);
        }
        instructions.Add(new CraftInstruction(recipe, craftsNeeded));
      }
    }

    return instructions;
  }
}