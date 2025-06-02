namespace VRisingCraftingPlanner.Models;

public readonly record struct ItemType(string Name, ItemOrigin Origin)
{
  public override string ToString() => Name;
};

public enum ItemOrigin
{
  Ingredient, 
  Product
}