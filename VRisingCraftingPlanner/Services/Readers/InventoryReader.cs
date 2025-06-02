using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using VRisingCraftingPlanner.DTOs;
using VRisingCraftingPlanner.Models;
using VRisingCraftingPlanner.Services.Stores;

namespace VRisingCraftingPlanner.Services.Readers;

public sealed class InventoryReader(ItemTypeStore itemTypeStore)
{
  public IEnumerable<Item> ReadInventory(string path)
  {
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

    var dtos = csv.GetRecords<InventoryEntryDto>().ToList();
    return dtos.Select(InventoryEntryDtoToItem);
  }

  private Item InventoryEntryDtoToItem(InventoryEntryDto dto) =>
    new(itemTypeStore.GetItemType(dto.Name), dto.Stored - dto.Desired);
}