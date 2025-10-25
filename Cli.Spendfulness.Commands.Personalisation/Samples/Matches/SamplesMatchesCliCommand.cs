using Cli.Commands.Abstractions;

namespace Cli.Spendfulness.Commands.Personalisation.Samples.Matches;

public class SamplesMatchesCliCommand : ICliCommand
{
    public static class ArgumentNames
    {
        public const string TransactionId = "transactionId";
    }
    
    public required string TransactionId { get; set; }
}