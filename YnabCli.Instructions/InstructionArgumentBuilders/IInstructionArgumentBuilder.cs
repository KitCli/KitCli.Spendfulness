using YnabCli.Instructions.InstructionArguments;

namespace YnabCli.Instructions.InstructionArgumentBuilders;

public interface IInstructionArgumentBuilder
{
    bool For(string argumentValue);

    InstructionArgument Create(string argumentName, string argumentValue);
}
