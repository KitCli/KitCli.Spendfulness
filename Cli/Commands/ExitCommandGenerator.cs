using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Commands;

public class ExitCommandGenerator : ICommandGenerator<ExitCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction) => new ExitCliCommand();
}