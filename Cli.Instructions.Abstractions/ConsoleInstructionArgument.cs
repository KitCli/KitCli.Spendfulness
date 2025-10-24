namespace Cli.Instructions.Abstractions;

public class ConsoleInstructionArgument(string argumentName)
{
    public string ArgumentName { get; set; } = argumentName;
}