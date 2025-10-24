using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Ynab.Commands.Reporting.SpareMoney.Help;

namespace Cli.Ynab.Commands.Reporting.SpareMoney;

public class SpareMoneyGenericCommandGenerator : ICommandGenerator<SpareMoneyCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        if (subCommandName == SpareMoneyCliCommand.SubCommandNames.Help)
        {
            return new SpareMoneyHelpCliCommand();
        }
        
        return GenerateDefaultCommand(arguments);
    }

    private static SpareMoneyCliCommand GenerateDefaultCommand(List<ConsoleInstructionArgument> arguments)
    {
        var addArgument = arguments.OfCurrencyType(SpareMoneyCliCommand.ArgumentNames.Add);
        var minusArgument = arguments.OfCurrencyType(SpareMoneyCliCommand.ArgumentNames.Minus);
        var minusSavingsArgument = arguments.OfType<bool>(SpareMoneyCliCommand.ArgumentNames.MinusSavings);

        return new SpareMoneyCliCommand
        {
            Add = addArgument?.ArgumentValue,
            Minus = minusArgument?.ArgumentValue,
            MinusSavings = minusSavingsArgument?.ArgumentValue
        };
    }
}