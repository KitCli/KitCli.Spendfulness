using Cli.Abstractions;

namespace YnabCli.Database;

public class YnabCliDbException(YnabCliDbExceptionCode code, string message)
    : CliException(CliExceptionCode.Custom, message)
{
    public new YnabCliDbExceptionCode Code = code;
}