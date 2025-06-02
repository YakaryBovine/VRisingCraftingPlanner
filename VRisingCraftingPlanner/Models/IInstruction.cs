namespace VRisingCraftingPlanner.Models;

public interface IInstruction
{
  public string Message { get; }
  
  public int Priority { get; }
}