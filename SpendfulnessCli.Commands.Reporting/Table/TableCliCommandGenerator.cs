using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Properties;
using Cli.Instructions.Abstractions;
using SpendfulnessCli.Aggregation.Aggregates;

namespace SpendfulnessCli.Commands.Reporting.Table;

public class TableCliCommandGenerator : IContinuousCliCommandGenerator<TableCliCommand>
{
    public CliCommand Generate(CliInstruction instruction, IEnumerable<CliCommandProperty> properties)
    {
        var x = properties
            .OfType<AggregateCliCommandProperty<IEnumerable<TransactionMonthTotalAggregate>>>()
            .FirstOrDefault();

        if (x != null)
        {
            return new TransactionMonthTotalAggregateTableCliCommand(x.Value);
        }
        
        return new TableCliCommand();
    }
}