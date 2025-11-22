using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using SpendfulnessCli.Commands.Personalisation.Transactions.List;

namespace SpendfulnessCli.Commands.Personalisation.Transactions;

[FactoryFor(typeof(TransactionsCliCommand))]
public class ListTransactionCliCommandFactory : ICliCommandFactory<TransactionsCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName == TransactionsCliCommand.SubCommandNames.List;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
    {
        var payeeNameArgument = instruction
            .Arguments
            .OfType<string>(ListTransactionCliCommand.ArgumentNames.PayeeName);

        return new ListTransactionCliCommand
        {
            PayeeName = payeeNameArgument?.ArgumentValue
        };
    }
}