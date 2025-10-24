using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Samples.Matches;

namespace Cli.Spendfulness.Commands.Personalisation.Samples;

public class SamplesCommandGenerator : ICommandGenerator<SamplesCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
        => subCommandName switch
        {
            SamplesCliCommand.SubCommandNames.Matches => GetMatchesCommand(arguments),
            _ => new SamplesCliCommand()
        };

    private SamplesMatchesCliCommand GetMatchesCommand(List<ConsoleInstructionArgument> arguments)
    {
        var trnasactionIdArgument = arguments
            .OfRequiredStringFrom<Guid, string>(SamplesMatchesCliCommand.ArgumentNames.TransactionId);

        return new SamplesMatchesCliCommand
        {
            TransactionId = trnasactionIdArgument.ArgumentValue
        };
    }
}