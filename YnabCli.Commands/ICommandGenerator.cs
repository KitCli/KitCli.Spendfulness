using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands;

public interface ICommandGenerator
{
    ICommand Generate(string? subCommandName, List<InstructionArgument> arguments);
}