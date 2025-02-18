namespace YnabCli.Instructions;

public record InstructionTokens(
    string? PrefixToken,
    string NameToken,
    Dictionary<string, string> ArgumentTokens);