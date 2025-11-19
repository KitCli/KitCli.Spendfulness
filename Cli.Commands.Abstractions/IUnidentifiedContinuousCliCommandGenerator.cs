using Cli.Instructions.Abstractions;

namespace Cli.Commands.Abstractions;

public interface IUnidentifiedContinuousCliCommandGenerator
{
    CliCommand Generate(CliInstruction instruction, IEnumerable<CliCommandProperty> properties);
}