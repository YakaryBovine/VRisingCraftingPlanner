using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Services;

public sealed class InventoryManager
{
  private readonly List<Item> _items = [];
  
  public List<Item> GetAllItems() => _items;

  public void Add(IEnumerable<Item> items) => Add(items, 1);

  public void Subtract(IEnumerable<Item> items) => Add(items, -1);

  private void Add(IEnumerable<Item> items, int countMultiplier)
  {
    foreach (var item in items)
    {
      var existingItem = _items.FirstOrDefault();
      if (existingItem != default)
      {
        _items.Remove(existingItem);
        _items.Add(existingItem with { Count = item.Count * countMultiplier });
      }
      else
        _items.Add(item with { Count = item.Count * countMultiplier });
    }
  }
}