using Cli.Instructions.Abstractions;

namespace Cli.Instructions;

public record ConsoleInstruction(
    string? Prefix,
    string? Name,
    string? SubName,
    IEnumerable<ConsoleInstructionArgument> Arguments);