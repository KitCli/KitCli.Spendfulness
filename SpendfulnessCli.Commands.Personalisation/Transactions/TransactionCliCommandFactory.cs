using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using SpendfulnessCli.Commands.Personalisation.Transactions.List;

namespace SpendfulnessCli.Commands.Personalisation.Transactions;

public class TransactionCliCommandFactory : ICliCommandFactory<TransactionsCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName is null;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new ListTransactionCliCommand();
}