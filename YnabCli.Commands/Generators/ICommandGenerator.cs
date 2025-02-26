using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Generators;

public interface ICommandGenerator
{
    ICommand Generate(string? subCommandName, List<InstructionArgument> arguments);
}