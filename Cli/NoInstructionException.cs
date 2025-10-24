namespace Cli;

public class NoInstructionException : CliException
{
    public NoInstructionException(string message) : base(CliExceptionCode.NoInstruction, message)
    {
    }
}