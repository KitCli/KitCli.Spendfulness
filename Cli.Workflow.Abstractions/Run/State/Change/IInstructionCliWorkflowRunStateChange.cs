using Cli.Instructions.Abstractions;

namespace Cli.Workflow.Abstractions.Run.State.Change;

public interface IInstructionCliWorkflowRunStateChange : ICliWorkflowRunStateChange
{
    CliInstruction Instruction { get; }
}