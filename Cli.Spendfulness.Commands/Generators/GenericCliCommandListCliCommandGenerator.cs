using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Spendfulness.Commands.Generators;

public class GenericCliCommandListCliCommandGenerator : ICliCommandGenerator<CliCommandListCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
    {
        return new CliCommandListCliCommand();
    }
}