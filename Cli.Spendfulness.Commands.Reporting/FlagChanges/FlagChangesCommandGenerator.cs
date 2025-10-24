using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace Cli.Ynab.Commands.Reporting.FlagChanges;

public class FlagChangesCommandGenerator : ICommandGenerator<FlagChangesCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        // TODO: I dont like that this isnt more generic!
        var from = arguments.OfType<DateOnly>(FlagChangesCliCommand.ArgumentNames.From);
        var to = arguments.OfType<DateOnly>(FlagChangesCliCommand.ArgumentNames.To);

        return new FlagChangesCliCommand
        {
            From = from?.ArgumentValue,
            To = to?.ArgumentValue
        };
    }
}