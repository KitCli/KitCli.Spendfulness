using Cli.Abstractions;
using Cli.Abstractions.Aggregators;
using SpendfulnessCli.Aggregation.Aggregates;

namespace SpendfulnessCli.Commands.Reusable.Table.MonthlySpending;

public record MonthlySpendingTableCliCommand(CliListAggregator<TransactionMonthTotalAggregate> Aggregator)
    : TableCliCommand;