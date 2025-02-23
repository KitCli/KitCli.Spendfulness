namespace YnabCli.Instructions.Parsers;

public record InstructionTokens(
    string? CommandPrefixToken,
    string CommandNameToken,
    Dictionary<string, string?> ArgumentTokens);