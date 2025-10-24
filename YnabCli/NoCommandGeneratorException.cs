using Ynab.Exceptions;

namespace YnabCli;

public class NoCommandGeneratorException : YnabException
{
    public NoCommandGeneratorException(string message) : base(YnabExceptionCode.NoCommandGenerator, message)
    {
    }
}