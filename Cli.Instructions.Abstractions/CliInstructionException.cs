using Cli.Abstractions;
using Cli.Abstractions.Exceptions;

namespace Cli.Instructions.Abstractions;

// TODO: Write unit test.
public class CliInstructionException : CliException
{
    public new readonly CliInstructionExceptionCode Code;

    public CliInstructionException(CliInstructionExceptionCode code, string message)
        : base(CliExceptionCode.Instruction, message)
    {
        Code = code;
    }
}