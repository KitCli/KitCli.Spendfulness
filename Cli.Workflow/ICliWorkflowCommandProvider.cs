using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Workflow;

public interface ICliWorkflowCommandProvider
{
    CliCommand GetCommand(CliInstruction instruction, List<CliCommandProperty> properties);
}