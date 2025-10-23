namespace Cli.Instructions.Extraction;

public record ConsoleInstructionTokenExtraction(
    string PrefixToken,
    string NameToken,
    string? SubNameToken,
    Dictionary<string, string?> ArgumentTokens);