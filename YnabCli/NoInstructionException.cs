using Ynab.Exceptions;

namespace YnabCli;

public class NoInstructionException : YnabException
{
    public NoInstructionException(string message) : base(YnabExceptionCode.NoInstruction, message)
    {
    }
}