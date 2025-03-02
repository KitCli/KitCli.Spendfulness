using ConsoleTables;
using Ynab;
using Ynab.Extensions;
using YnabCli.Commands.Factories;
using YnabCli.Commands.Handlers;
using YnabCli.ViewModels.Aggregates;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.Extensions;
using YnabCli.ViewModels.ViewModelBuilders;

namespace YnabCli.Commands.Reporting.RecurringTransactions;

public class RecurringTransactionsCommandHandler : CommandHandler, ICommandHandler<RecurringTransactionsCommand>
{
    private readonly CommandBudgetGetter _budgetGetter;
    private readonly TransactionMemoOccurrenceViewModelBuilder _viewModelBuilder;

    public RecurringTransactionsCommandHandler(
        CommandBudgetGetter budgetGetter,
        TransactionMemoOccurrenceViewModelBuilder viewModelBuilder)
    {
        _budgetGetter = budgetGetter;
        _viewModelBuilder = viewModelBuilder;
    }

    private const int DefaultMinimumOccurrences = 2;
    
    public async Task<ConsoleTable> Handle(RecurringTransactionsCommand command, CancellationToken cancellationToken)
    {
        var aggregator = await PrepareAggregator(command);

        var viewModel = _viewModelBuilder
            .WithAggregator(aggregator)
            .Build();

        return Compile(viewModel);
    }

    private async Task<ListAggregator<TransactionMemoOccurrenceAggregate>> PrepareAggregator(RecurringTransactionsCommand command)
    {
        var budget =  await _budgetGetter.Get();
        var transactions = await budget.GetTransactions();
        
        var aggregator = new TransactionMemoOccurrenceAggregator(transactions)
            .WhereTransactions(ts => ts.FilterOutCategories([YnabConstants.SplitCategoryId]));

        if (command.From.HasValue)
        {
            aggregator.WhereTransactions(ts => ts.FilterFrom(command.From.Value));
        }
        
        if (command.To.HasValue)
        {
            aggregator.WhereTransactions(ts => ts.FilterTo(command.To.Value));
        }

        if (command.PayeeName != null)
        {
            aggregator.WhereTransactions(ts => ts.FilterByPayeeName(command.PayeeName));
        }
        
        return aggregator
            .WhereAggregates(a => FilterByMinimumOccurrences(command.MinimumOccurrences, a))
            .WhereAggregates(SortByMinimumOccurrence);
    }

    private IEnumerable<TransactionMemoOccurrenceAggregate> FilterByMinimumOccurrences(int? minimumOccurrences,
        IEnumerable<TransactionMemoOccurrenceAggregate> aggregates)
            => aggregates.FilterByMinimumOccurrences(minimumOccurrences ?? DefaultMinimumOccurrences);
    
    private IEnumerable<TransactionMemoOccurrenceAggregate> SortByMinimumOccurrence(
        IEnumerable<TransactionMemoOccurrenceAggregate> aggregates)
            => aggregates.OrderByDescending(aggregate => aggregate.MemoOccurrence);
}