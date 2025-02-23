using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands;

public interface ICommandGenerator
{
    ICommand Generate(List<InstructionArgument> arguments);
}