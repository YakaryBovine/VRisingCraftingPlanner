namespace VRisingCraftingPlanner.Data;

public readonly record struct ItemType(string Name)
{
  public override string ToString() => Name;
};