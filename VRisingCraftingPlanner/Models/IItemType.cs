namespace VRisingCraftingPlanner.Models;

/// <summary>
/// A type of item, such as Ember Glass or Iron Ingot.
/// </summary>
/// <param name="Name">Name of the item.</param>
/// <param name="Origin">Where the item comes from, e.g. the overworld or crafting.</param>
/// <param name="Tier">How many steps it takes to create the item. Used to determine instruction ordering.</param>
public readonly record struct ItemType(string Name, ItemOrigin Origin, int Tier)
{
  public override string ToString() => Name;
}

public enum ItemOrigin
{
  Ingredient, 
  Product
}