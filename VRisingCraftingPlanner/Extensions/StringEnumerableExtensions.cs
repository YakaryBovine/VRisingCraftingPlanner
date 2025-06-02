namespace VRisingCraftingPlanner.Extensions;

public static class StringEnumerableExtensions
{
  public static string JoinWithAnd(this IEnumerable<string> items)
  {
    var list = items.ToList();
    return list.Count switch
    {
      0 => "",
      1 => list[0],
      2 => $"{list[0]} and {list[1]}",
      _ => string.Join(", ", list.Take(list.Count - 1)) + $" and {list.Last()}"
    };
  }
}