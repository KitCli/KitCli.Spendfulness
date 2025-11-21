using Cli.Instructions.Abstractions;

namespace Cli.Workflow.Abstractions.Run.State.Change;

public class InstructionCliWorkflowRunStateChange(
    TimeSpan at,
    ClIWorkflowRunStateStatus from,
    ClIWorkflowRunStateStatus to,
    CliInstruction instruction)
    : CliWorkflowRunStateChange(at, from, to)
{
    public readonly CliInstruction Instruction = instruction;
}