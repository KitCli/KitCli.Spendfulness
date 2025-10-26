using Cli.Instructions.Abstractions;

namespace Cli.Commands.Abstractions;

public interface IUnidentifiedCliCommandGenerator
{
    ICliCommand Generate(CliInstruction instruction);
}