using VRisingCraftingPlanner.Models;
using VRisingCraftingPlanner.Services.Readers;

namespace VRisingCraftingPlanner.Services.Stores;

public sealed class InventoryStore(InventoryReader reader)
{
  private readonly List<Item> _inventory = reader.ReadInventory("Data/inventory.csv").ToList();

  public IReadOnlyList<Item> GetInventory() => _inventory;
}