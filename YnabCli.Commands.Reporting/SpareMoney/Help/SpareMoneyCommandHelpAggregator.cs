using YnabCli.Commands.Aggregate;
using YnabCli.Commands.Aggregator;

namespace YnabCli.Commands.Reporting.SpareMoney.Help;

public class SpareMoneyCommandHelpAggregator : CommandHelpAggregator
{
    protected override List<CommandHelpAggregate> AggregateForCommand()
        => [
            new(
                "/spare-money",
                CommandActionType.Command,
                "Get spare money after critical expenses"),

            new(
                "help",
                CommandActionType.SubCommand,
                "Get a list of all calls"),

            new(
                "--minus-savings",
                CommandActionType.Argument,
                "Get spare moeny after critical expenses, minus savings")
        ];

}