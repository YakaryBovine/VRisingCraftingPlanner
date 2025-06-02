using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Services;

public sealed class ItemTypeStore(ItemTypesParser parser)
{
  private readonly List<ItemType> _itemTypes = parser.LoadItemTypes("Data/items.csv");

  public ItemType GetItemType(string name)
  {
    var itemType = _itemTypes.FirstOrDefault(x => x.Name == name);
    if (itemType == default)
      throw new InvalidOperationException($"There is no item type named {name}. Make sure it's included in items.csv");
    return itemType;
  }
}