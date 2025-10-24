using Cli.Instructions.Abstractions;

namespace Cli.Commands.Abstractions;

public interface IGenericCommandGenerator
{
    ICliCommand Generate(CliInstruction instruction);
}