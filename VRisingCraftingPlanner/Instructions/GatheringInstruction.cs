using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Instructions;

public sealed record GatheringInstruction(ItemType ItemType, int Count, string Location) : IInstruction
{
  public string Message => $"Gather {Count} {ItemType} from {Location}";

  public int Priority => 1000;
}