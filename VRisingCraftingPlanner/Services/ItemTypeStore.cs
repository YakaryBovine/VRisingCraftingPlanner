using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using VRisingCraftingPlanner.Data;

namespace VRisingCraftingPlanner.Services;

public sealed class ItemTypeStore
{
  public List<ItemType> LoadItemTypes(string path)
  {
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
    {
      HeaderValidated = null,
      MissingFieldFound = null
    });

    return csv.GetRecords<ItemType>().ToList();
  }
}