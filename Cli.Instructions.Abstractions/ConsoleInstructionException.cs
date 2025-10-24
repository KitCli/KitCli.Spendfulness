using Cli.Abstractions;
using Cli.Instructions.Exceptions;

namespace Cli.Instructions.Abstractions;

public class ConsoleInstructionException(ConsoleInstructionExceptionCode code, string message)
    : CliException(CliExceptionCode.Command, message)
{
    public new readonly ConsoleInstructionExceptionCode Code = code;
}