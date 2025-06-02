namespace VRisingCraftingPlanner.Data;

public sealed record Recipe(Item[] Components, Item[] Products, string Station);