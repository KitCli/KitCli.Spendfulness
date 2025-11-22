using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Reporting.SpareMoney;

public class SpareMoneyCliCommandFactory : ICliCommandFactory<SpareMoneyCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var addArgument = instruction.Arguments.OfCurrencyType(SpareMoneyCliCommand.ArgumentNames.Add);
        var minusArgument = instruction.Arguments.OfCurrencyType(SpareMoneyCliCommand.ArgumentNames.Minus);
        var minusSavingsArgument = instruction.Arguments.OfType<bool>(SpareMoneyCliCommand.ArgumentNames.MinusSavings);

        return new SpareMoneyCliCommand
        {
            Add = addArgument?.ArgumentValue,
            Minus = minusArgument?.ArgumentValue,
            MinusSavings = minusSavingsArgument?.ArgumentValue
        };
    }
}