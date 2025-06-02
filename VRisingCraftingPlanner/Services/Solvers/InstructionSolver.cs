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

    ProcessInventory();
    ProcessCrafts(instructions);
    ProcessMaterials(instructions);
    PrintInstructions(instructions);
  }

  private void ProcessInventory()
  {
    itemBalanceStore.Add(inventoryStore.GetInventory());
  }

  private void ProcessCrafts(List<IInstruction> instructions)
  {
    foreach (var item in itemBalanceStore.GetAllItems().Where(x => x.ItemType.Origin == ItemOrigin.Product).ToList())
    {
      ProcessCraft(item, instructions, 1);
    }
  }

  private void ProcessCraft(Item item, List<IInstruction> instructions, int priority)
  {
    if (item.Count < 0)
    {
      var recipe = recipeStore.GetRecipeForItem(item.ItemType);
      var productCount = recipe.Products.First(x => x.ItemType == item.ItemType).Count;
      var craftsNeeded = item.Count / productCount * -1;
      for (var i = 0; i < craftsNeeded; i++)
      {
        itemBalanceStore.Add(recipe.Products);
        itemBalanceStore.Subtract(recipe.Ingredients);
      }
      instructions.Add(new CraftInstruction(recipe, craftsNeeded, priority));

      foreach (var subProduct in recipe.Ingredients.Where(x => x.ItemType.Origin == ItemOrigin.Product))
      {
        ProcessCraft(subProduct with { Count = subProduct.Count * item.Count / productCount }, instructions, priority + 1);
      }
    }
  }
  
  private void ProcessMaterials(List<IInstruction> instructions)
  {
    foreach (var item in itemBalanceStore.GetAllItems().Where(x => x.ItemType.Origin == ItemOrigin.Ingredient).ToList())
      if (item.Count < 0) 
        instructions.Add(new GatheringInstruction(item.ItemType, item.Count * -1, "Farbane"));
  }
  
  private static void PrintInstructions(List<IInstruction> instructions)
  {
    var count = 1;
    foreach (var instruction in instructions.OrderByDescending(x => x.Priority))
    {
      Console.WriteLine($"{count}. {instruction.Message}");
      count++;
    }
  }
}