using VRisingCraftingPlanner.Data;

namespace VRisingCraftingPlanner.Services;

public sealed class RecipeBook
{
  private readonly Dictionary<ItemType, Recipe> _recipesByItem = new();
  
  public RecipeBook(IEnumerable<Recipe> recipes)
  {
    foreach (var recipe in recipes)
      foreach (var product in recipe.Products) 
        _recipesByItem.Add(product.ItemType, recipe);
  }

  public Recipe GetRecipeForItem(ItemType itemType) => _recipesByItem[itemType];
}