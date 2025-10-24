using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Transactions.List;

namespace Cli.Spendfulness.Commands.Personalisation.Transactions;

public class TransactionCommandGenerator : ICommandGenerator<TransactionsCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
        => instruction.SubInstructionName switch
        {
            TransactionsCliCommand.SubCommandNames.List => CreateListCommand(instruction.Arguments),
            _ => new TransactionsListCliCommand(),
        };
    
    private TransactionsListCliCommand CreateListCommand(List<CliInstructionArgument> arguments)
    {
        var payeeNameArgument = arguments.OfType<string>(TransactionsListCliCommand.ArgumentNames.PayeeName);

        return new TransactionsListCliCommand
        {
            PayeeName = payeeNameArgument?.ArgumentValue
        };
    }
}