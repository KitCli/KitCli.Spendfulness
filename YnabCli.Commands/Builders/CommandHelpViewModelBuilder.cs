using YnabCli.Commands.Aggregate;
using YnabCli.Commands.Aggregator;
using YnabCli.ViewModels.ViewModelBuilders;

namespace YnabCli.Commands.Builders;

public class CommandHelpViewModelBuilder : ViewModelBuilder<CommandHelpAggregator, List<CommandHelpAggregate>>
{
    protected override List<string> BuildColumnNames(List<CommandHelpAggregate> evaluation)
        => [nameof(CommandHelpAggregate.Call), nameof(CommandHelpAggregate.Type), nameof(CommandHelpAggregate.Summary)];

    protected override List<List<object>> BuildRows(List<CommandHelpAggregate> aggregates)
    {
        var rows = aggregates
            .Select(aggregate => new List<object>
            {
                aggregate.Call,
                aggregate.Type.ToString(),
                aggregate.Summary,
            })
            .ToList();

        return rows;
    }
}