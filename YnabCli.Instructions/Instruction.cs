using YnabCli.Instructions.Arguments;

namespace YnabCli.Instructions;

public record Instruction(
    string? Prefix,
    string? Name,
    string? SubName,
    IEnumerable<InstructionArgument>? Arguments);