namespace Cli;

public class CliException : Exception
{
    public CliExceptionCode Code { get; }

    public CliException(CliExceptionCode code, string message) : base(message)
    {
        Code = code;
    }
}