namespace Ynab.Exceptions;

public class YnabExceptin : Exception
{
    public YnabExceptionCode Code { get; }
    
    public YnabExceptin(YnabExceptionCode code, string message) : base(message)
    {
        Code = code;
    }
}