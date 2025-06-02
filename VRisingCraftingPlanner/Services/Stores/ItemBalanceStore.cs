using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Services.Stores;

/// <summary>
/// Measures how many items are available at a given point in time, e.g. before crafting is done or after crafting is done.
/// </summary>
public sealed class ItemBalanceStore
{
  private readonly List<Item> _items = [];
  
  public IReadOnlyList<Item> GetAllItems() => _items;

  public void Add(IEnumerable<Item> items) => Add(items, 1);

  public void Subtract(IEnumerable<Item> items) => Add(items, -1);

  private void Add(IEnumerable<Item> items, int countMultiplier)
  {
    foreach (var item in items)
    {
      var existingIndex = _items.FindIndex(i => i.ItemType == item.ItemType);
      if (existingIndex >= 0)
      {
        var existing = _items[existingIndex];
        var newCount = existing.Count + (item.Count * countMultiplier);
        if (newCount == 0)
          _items.RemoveAt(existingIndex);
        else
          _items[existingIndex] = existing with { Count = newCount };
      }
      else
      {
        _items.Add(item with { Count = item.Count * countMultiplier });
      }
    }
  }
}