using Cli.Abstractions;
using Cli.Abstractions.Exceptions;

namespace Spendfulness.Database.Abstractions;

public class SpendfulnessDbException : CliException
{
    public SpendfulnessDbExceptionCode InnerCode { get; }
    
    public SpendfulnessDbException(SpendfulnessDbExceptionCode code, string message)
        : base(CliExceptionCode.Custom, message)
    {
        InnerCode = code;
    }
}