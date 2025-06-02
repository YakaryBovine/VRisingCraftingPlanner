using VRisingCraftingPlanner.Extensions;

namespace VRisingCraftingPlanner.Data;

public sealed class CraftInstruction(Recipe recipe, int count) : IInstruction
{
  public string Message
  {
    get {     
      var products = recipe.Products.Select(p => $"{p.Count * count} {p.ItemType}").JoinWithAnd();
      var materials = recipe.Components.Select(m => $"{m.Count * count} {m.ItemType}").JoinWithAnd();
      return $"Craft {products} using {materials} at the {recipe.Station}."; }
  }
}