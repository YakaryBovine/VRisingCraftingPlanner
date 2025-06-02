using VRisingCraftingPlanner.Extensions;
using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Instructions;

public sealed class CraftInstruction(Recipe recipe, int count, int priority) : IInstruction
{
  public string Message
  {
    get {     
      var products = Recipe.Products.Select(p => $"{p.Count * Count} {p.ItemType}").JoinWithAnd();
      var materials = Recipe.Ingredients.Select(m => $"{m.Count * Count} {m.ItemType}").JoinWithAnd();
      return $"Craft {products} with {materials} at the {Recipe.Station}"; }
  }

  public int Priority { get; } = priority;

  public Recipe Recipe { get; } = recipe;

  public int Count { get; set; } = count;
}