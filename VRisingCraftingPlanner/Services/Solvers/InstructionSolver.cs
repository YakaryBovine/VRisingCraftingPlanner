using VRisingCraftingPlanner.Instructions;
using VRisingCraftingPlanner.Models;
using VRisingCraftingPlanner.Services.Stores;

namespace VRisingCraftingPlanner.Services.Solvers;

public sealed class InstructionSolver(
  InventoryStore inventoryStore,
  RecipeStore recipeStore,
  ItemBalanceStore itemBalanceStore,
  InstructionStore instructions)
{
  /// <summary>
  /// Receives the items the player would like to acquire, and the items they already have, and provides a series of
  /// instructions for acquiring those items.
  /// </summary>
  /// <returns>A series of tasks the player should undertake to acquire the items.</returns>
  public void Solve()
  {
    ProcessInventory();
    ProcessCrafts();
    GenerateGatherInstructions();
    PrintInstructions();
  }

  private void ProcessInventory()
  {
    itemBalanceStore.Add(inventoryStore.GetInventory());
  }

  private void ProcessCrafts()
  {
    while (true)
    {
      var itemsToProcess = itemBalanceStore.GetAllItems()
        .Where(x => x.ItemType.Origin == ItemOrigin.Product)
        .Where(x => x.Count < 0)
        .ToList();

      if (itemsToProcess.Count > 0)
      {
        foreach (var item in itemsToProcess) 
          ProcessCraft(item);

        continue;
      }

      break;
    }
  }

  private void ProcessCraft(Item item)
  {
    if (item.Count < 0)
    {
      var recipe = recipeStore.GetRecipeForItem(item.ItemType);
      var productCount = recipe.Products.First(x => x.ItemType == item.ItemType).Count;
      
      var craftsNeeded = (int)Math.Ceiling((double)item.Count / productCount * -1);
      for (var i = 0; i < craftsNeeded; i++)
      {
        itemBalanceStore.Subtract(recipe.Ingredients);
        itemBalanceStore.Add(recipe.Products);
      }
      
      instructions.Add(new CraftInstruction(recipe, craftsNeeded, item.ItemType.Tier));
    }
  }
  
  private void GenerateGatherInstructions()
  {
    foreach (var item in itemBalanceStore.GetAllItems().Where(x => x.ItemType.Origin == ItemOrigin.Ingredient).ToList())
      if (item.Count < 0) 
        instructions.Add(new GatheringInstruction(item.ItemType, item.Count * -1));
  }
  
  private void PrintInstructions()
  {
    var count = 1;
    foreach (var instruction in instructions.GetAll().OrderBy(x => x.Priority))
    {
      Console.WriteLine($"{count}. {instruction.Message}");
      count++;
    }
  }
}