namespace Cli.Instructions.Abstractions;

public class CliInstructionArgument(string argumentName)
{
    public string ArgumentName { get; set; } = argumentName;
}