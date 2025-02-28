namespace YnabCli.Database;

public class YnabCliDbException : Exception
{
    public YnabCliDbExceptionCode Code { get; }
    
    public YnabCliDbException(YnabCliDbExceptionCode code, string message) : base(message)
    {
        Code = code;
    }
}