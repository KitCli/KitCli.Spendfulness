using Cli.Commands.Abstractions;
using Cli.Outcomes;
using ConsoleTables;
using YnabCli.Abstractions;
using YnabCli.Commands.Builders;
using YnabCli.Commands.Handlers;

namespace YnabCli.Commands.Reporting.SpareMoney.Help;

public class SpareMoneyHelpCommandHandler(CommandHelpViewModelBuilder commandHelpViewModelBuilder)
    : CommandHandler, ICommandHandler<SpareMoneyHelpCommand>
{
    public Task<CliCommandOutcome> Handle(SpareMoneyHelpCommand request, CancellationToken cancellationToken)
    {
        var aggregator = new SpareMoneyCommandHelpAggregator();
        
        var viewModel = commandHelpViewModelBuilder
            .WithAggregator(aggregator)
            .WithRowCount(false)
            .Build();
        
        var compilation = Compile(viewModel);

        return Task.FromResult<CliCommandOutcome>(compilation);
    }
}