using VRisingCraftingPlanner.Instructions;
using VRisingCraftingPlanner.Models;

namespace VRisingCraftingPlanner.Services.Stores;

public sealed class InstructionStore
{
  private readonly List<IInstruction> _instructions = [];

  public void Add(IInstruction instruction)
  {
    if (instruction is CraftInstruction craftInstruction)
    {
      var existing = _instructions
        .Where(x => x is CraftInstruction)
        .Cast<CraftInstruction>()
        .FirstOrDefault(x => x.Recipe == craftInstruction.Recipe);

      if (existing is not null)
      {
        existing.Count += craftInstruction.Count;
        return;
      }
    }
    
    _instructions.Add(instruction);
  }

  public IReadOnlyList<IInstruction> GetAll() => _instructions;
}