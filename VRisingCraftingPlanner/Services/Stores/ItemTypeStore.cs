using VRisingCraftingPlanner.Models;
using VRisingCraftingPlanner.Services.Readers;

namespace VRisingCraftingPlanner.Services.Stores;

public sealed class ItemTypeStore(ItemTypesReader reader)
{
  private readonly List<ItemType> _itemTypes = reader.LoadItemTypes("Data/items.csv");

  public ItemType GetItemType(string name)
  {
    var itemType = _itemTypes.FirstOrDefault(x => x.Name == name);
    if (itemType == default)
      throw new InvalidOperationException($"There is no item type named {name}. Make sure it's included in items.csv");
    return itemType;
  }
}