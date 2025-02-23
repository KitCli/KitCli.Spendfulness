using YnabCli.Instructions.Arguments;

namespace YnabCli.Instructions;

public record Instruction(
    string? Prefix,
    string Name,
    IEnumerable<InstructionArgument> Arguments);