using Cli.Instructions.Abstractions;
using Cli.Workflow.Abstractions;
using Cli.Workflow.Abstractions.Run.State.Change;

namespace Cli.Workflow.Run.State.Change;

public class InstructionCliWorkflowRunStateChange : CliWorkflowRunStateChange, IInstructionCliWorkflowRunStateChange
{
    public CliInstruction Instruction { get;  }

    public InstructionCliWorkflowRunStateChange(
        TimeSpan at,
        ClIWorkflowRunStateStatus from,
        ClIWorkflowRunStateStatus to,
        CliInstruction instruction)
        : base(at, from, to)
    {
        Instruction = instruction;
    }
}