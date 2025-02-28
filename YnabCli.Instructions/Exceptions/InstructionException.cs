namespace YnabCli.Instructions.Exceptions;

public class InstructionException : Exception
{
    public InstructionExceptionCode Code { get; }
    
    public InstructionException(InstructionExceptionCode code, string message) : base(message)
    {
        Code = code;
    }
}