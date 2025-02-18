
using YnabCli.Instructions.InstructionArguments;

namespace YnabCli.Instructions.InstructionArgumentBuilders;

public class StringInstructionArgumentBuilder : IInstructionArgumentBuilder
{
    public bool For(string argumentValue)
        => argumentValue
            .ToCharArray()
            .Where(x => !char.IsWhiteSpace(x))
            .All(char.IsLetter);

    public InstructionArgument Create(string argumentName, string argumentValue)
        => new TypedInstructionArgument<string>(argumentName, argumentValue);
}