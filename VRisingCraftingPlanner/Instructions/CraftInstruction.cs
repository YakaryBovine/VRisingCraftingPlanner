using VRisingCraftingPlanner.Extensions;
using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Instructions;

public sealed class CraftInstruction(Recipe recipe, int count, int priority) : IInstruction
{
  public string Message
  {
    get {     
      var products = recipe.Products.Select(p => $"{p.Count * count} {p.ItemType}").JoinWithAnd();
      var materials = recipe.Ingredients.Select(m => $"{m.Count * count} {m.ItemType}").JoinWithAnd();
      return $"Craft {products} with {materials} at the {recipe.Station}"; }
  }

  public int Priority { get; } = priority;
}