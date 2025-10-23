using Cli.Instructions.Arguments;

namespace YnabCli.Commands.Generators;

public interface IGenericCommandGenerator
{
    ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments);
}