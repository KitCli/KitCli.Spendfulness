namespace Cli.Instructions.Indexers;

public class CliInstructionTokens : Dictionary<CliInstructionTokenType, string> 
{
    public string Prefix => this[CliInstructionTokenType.Prefix];
    public string Name => this[CliInstructionTokenType.Name];
    public string? SubName => this[CliInstructionTokenType.SubName];
    public string? Arguments => this[CliInstructionTokenType.Arguments];
}