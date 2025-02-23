using YnabCli.Instructions.Arguments;

namespace YnabCli.Instructions.Builders;

public interface IInstructionArgumentBuilder
{
    bool For(string? argumentValue);

    InstructionArgument Create(string argumentName, string? argumentValue);
}
