using YnabCli.Instructions.InstructionArguments;

namespace YnabCli.Commands;

public interface ICommandGenerator
{
    ICommand Generate(List<InstructionArgument> arguments);
}