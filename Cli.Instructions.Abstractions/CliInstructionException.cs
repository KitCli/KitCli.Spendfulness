using Cli.Abstractions;

namespace Cli.Instructions.Abstractions;

public class CliInstructionException(CliInstructionExceptionCode code, string message)
    : CliException(CliExceptionCode.Command, message)
{
    public new readonly CliInstructionExceptionCode Code = code;
}