using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Services.Readers;

public class ItemTypesReader
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