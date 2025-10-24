namespace Cli.Instructions.Abstractions;

public record ConsoleInstruction(
    string? Prefix,
    string? Name,
    string? SubName,
    IEnumerable<ConsoleInstructionArgument> Arguments);