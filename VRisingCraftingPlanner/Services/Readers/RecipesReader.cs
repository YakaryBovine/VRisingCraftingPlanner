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
  
  public IEnumerable<Recipe> LoadRecipes(string folderPath)
  {
    var recipes = new List<Recipe>();

    foreach (var file in Directory.GetFiles(folderPath, "*.json"))
    {
      var json = File.ReadAllText(file);
      var recipeDtos = JsonSerializer.Deserialize<List<RecipeDto>>(json, _options);
      if (recipeDtos != null) 
        recipes.AddRange(recipeDtos.Select(RecipeDtoToRecipe));
    }

    return recipes;
  }

  private Recipe RecipeDtoToRecipe(RecipeDto recipeDto) => new(recipeDto.Ingredients.Select(ItemDtoToItem),
    recipeDto.Products.Select(ItemDtoToItem), recipeDto.Station, recipeDto.Tier);

  private Item ItemDtoToItem(ItemDto dto)
  {
    return new Item(itemTypeStore.GetItemType(dto.ItemType), dto.Count);
  }
}