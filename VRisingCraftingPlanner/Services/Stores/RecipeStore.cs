using VRisingCraftingPlanner.Models;
using VRisingCraftingPlanner.Services.Readers;

namespace VRisingCraftingPlanner.Services.Stores;

public sealed class RecipeStore
{
  private readonly Dictionary<ItemType, Recipe> _recipesByItem = new();
  
  public RecipeStore(RecipesReader recipesReader)
  {
    var recipes = recipesReader.LoadRecipes("Data/recipes.json");
    foreach (var recipe in recipes)
      foreach (var product in recipe.Products) 
        _recipesByItem.Add(product.ItemType, recipe);
  }

  public Recipe GetRecipeForItem(ItemType itemType)
  {
    if (_recipesByItem.TryGetValue(itemType, out var recipe))
      return recipe;

    throw new Exception($"There is no recipe that can craft {itemType.Name}. Add it to recipes.json");
  }
}