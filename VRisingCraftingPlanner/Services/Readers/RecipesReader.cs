using System.Text.Json;
using VRisingCraftingPlanner.DTOs;
using VRisingCraftingPlanner.Models;
using VRisingCraftingPlanner.Services.Stores;

namespace VRisingCraftingPlanner.Services.Readers;

public sealed class RecipesReader(ItemTypeStore itemTypeStore)
{
  private readonly JsonSerializerOptions _options = new()
  {
    PropertyNameCaseInsensitive = true
  };
  
  public IEnumerable<Recipe> LoadRecipes(string path)
  {
    var json = File.ReadAllText(path);
    var recipeDtos = JsonSerializer.Deserialize<List<RecipeDto>>(json, _options)!;
    return recipeDtos.Select(RecipeDtoToRecipe);
  }

  private Recipe RecipeDtoToRecipe(RecipeDto recipeDto) => new(recipeDto.Ingredients.Select(ItemDtoToItem),
    recipeDto.Products.Select(ItemDtoToItem), recipeDto.Station);

  private Item ItemDtoToItem(ItemDto dto)
  {
    return new Item(itemTypeStore.GetItemType(dto.ItemType), dto.Count);
  }
}