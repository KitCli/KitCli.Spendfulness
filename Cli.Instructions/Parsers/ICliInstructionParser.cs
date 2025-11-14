using Cli.Instructions.Abstractions;

namespace Cli.Instructions.Parsers;

public interface ICliInstructionParser
{
    CliInstruction Parse(string terminalInput);
}