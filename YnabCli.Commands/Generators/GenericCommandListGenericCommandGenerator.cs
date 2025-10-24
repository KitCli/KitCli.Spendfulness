using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace YnabCli.Commands.Generators;

// TODO: Not convinced this is correct.
public class GenericCommandListGenericCommandGenerator : ICommandGenerator<CommandListCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new CommandListCommand();
    }
}