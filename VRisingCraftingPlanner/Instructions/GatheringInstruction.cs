using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Instructions;

public sealed record GatheringInstruction(ItemType ItemType, int Count) : IInstruction
{
  public string Message => $"Gather {Count} {ItemType}";

  public int Priority => 0;
}