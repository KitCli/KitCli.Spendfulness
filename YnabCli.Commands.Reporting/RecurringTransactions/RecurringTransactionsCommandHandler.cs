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
        var aggregator = await GetAggregator(command);

        var viewModel = _viewModelBuilder
            .WithAggregator(aggregator)
            .Build();

        return Compile(viewModel);
    }

    private async Task<ListAggregator<TransactionMemoOccurrenceAggregate>> GetAggregator(RecurringTransactionsCommand command)
    {
        var budget =  await _budgetGetter.Get();
        var transactions = await budget.GetTransactions();

        var nonSplitTransactions = transactions.FilterOutCategories([YnabConstants.SplitCategoryId]);
        
        if (command.From.HasValue)
        {
            nonSplitTransactions = nonSplitTransactions.FilterFrom(command.From.Value);
        }

        if (command.To.HasValue)
        {
            nonSplitTransactions = nonSplitTransactions.FilterTo(command.To.Value);
        }

        if (command.PayeeName != null)
        {
            nonSplitTransactions = nonSplitTransactions.FilterByPayeeName(command.PayeeName);
        }
        
        return new TransactionMemoOccurrenceAggregator(nonSplitTransactions)
            .AddAggregationOperation(a => FilterByMinimumOccurrences(command.MinimumOccurrences, a))
            .AddAggregationOperation(SortByMinimumOccurrence);
    }

    private IEnumerable<TransactionMemoOccurrenceAggregate> FilterByMinimumOccurrences(int? minimumOccurrences,
        IEnumerable<TransactionMemoOccurrenceAggregate> aggregates)
            => aggregates.FilterByMinimumOccurrences(minimumOccurrences ?? DefaultMinimumOccurrences);
    
    private IEnumerable<TransactionMemoOccurrenceAggregate> SortByMinimumOccurrence(
        IEnumerable<TransactionMemoOccurrenceAggregate> aggregates)
            => aggregates.OrderByDescending(aggregate => aggregate.MemoOccurrence);
}