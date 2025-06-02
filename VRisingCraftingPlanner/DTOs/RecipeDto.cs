namespace VRisingCraftingPlanner.DTOs;

public sealed record RecipeDto(ItemDto[] Ingredients, ItemDto[] Products, string Station, int Tier);