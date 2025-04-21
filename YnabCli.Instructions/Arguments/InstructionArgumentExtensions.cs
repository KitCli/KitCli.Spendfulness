using YnabCli.Instructions.Exceptions;

namespace YnabCli.Instructions.Arguments;

public static class InstructionArgumentExtensions
{
    public static TypedInstructionArgument<TArgumentType>? OfType<TArgumentType>(
        this IEnumerable<InstructionArgument> arguments, string argumentName) where TArgumentType : notnull
            => arguments
                .Where(argument => argument.ArgumentName == argumentName)
                .OfType<TypedInstructionArgument<TArgumentType>>()
                .FirstOrDefault();

    // TODO: Im not sure this returning the type is even worth it? Just return the value?
    public static TypedInstructionArgument<TArgumentType> OfRequiredType<TArgumentType>(
        this IEnumerable<InstructionArgument> arguments, string argumentName) where TArgumentType : notnull
    {
        var argument = OfType<TArgumentType>(arguments, argumentName);

        if (argument is null)
        {
            throw new InstructionException(InstructionExceptionCode.ArgumentIsRequired,
                $"Argument '{argumentName}' is required for this command.");
        }
        
        return argument;
    }
}