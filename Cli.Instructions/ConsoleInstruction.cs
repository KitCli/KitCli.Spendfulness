using Cli.Instructions.Arguments;

namespace Cli.Instructions;

public record ConsoleInstruction(
    string? Prefix,
    string? Name,
    string? SubName,
    IEnumerable<ConsoleInstructionArgument> Arguments);