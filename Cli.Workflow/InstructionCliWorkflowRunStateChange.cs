using Cli.Instructions.Abstractions;
using Cli.Workflow.Abstractions;

namespace Cli.Workflow;

public class InstructionCliWorkflowRunStateChange(
    ClIWorkflowRunStateType startedAt,
    ClIWorkflowRunStateType movedTo,
    CliInstruction instruction)
    : CliWorkflowRunStateChange(startedAt, movedTo)
{
    public readonly CliInstruction Instruction = instruction;
}