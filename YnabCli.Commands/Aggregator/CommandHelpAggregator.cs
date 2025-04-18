using YnabCli.Commands.Aggregate;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.Aggregator.AmountAggregators;

namespace YnabCli.Commands.Aggregator;

public abstract class CommandHelpAggregator : Aggregator<List<CommandHelpAggregate>>
{
    public override List<CommandHelpAggregate> Aggregate() => AggregateForCommand();
    
    protected abstract List<CommandHelpAggregate> AggregateForCommand();
}