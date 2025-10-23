namespace Cli.Instructions.Arguments;

public class ConsoleInstructionArgument(string argumentName)
{
    public string ArgumentName { get; set; } = argumentName;
}