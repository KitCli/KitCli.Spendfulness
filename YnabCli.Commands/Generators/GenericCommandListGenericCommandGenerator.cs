using Cli.Instructions.Arguments;

namespace YnabCli.Commands.Generators;

public class GenericCommandListGenericCommandGenerator : ICommandGenerator<CommandListCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new CommandListCommand();
    }
}