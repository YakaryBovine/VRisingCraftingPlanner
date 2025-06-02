using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Instructions;

public sealed record GatheringInstruction(ItemType ItemType, int Count, string Location);