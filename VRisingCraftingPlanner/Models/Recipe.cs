namespace VRisingCraftingPlanner.Models;

public sealed record Recipe(IEnumerable<Item> Ingredients, IEnumerable<Item> Products, string Station);