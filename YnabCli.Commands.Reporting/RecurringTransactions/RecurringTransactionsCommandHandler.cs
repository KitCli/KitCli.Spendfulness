using ConsoleTables;
using Ynab;
using Ynab.Clients;
using Ynab.Extensions;
using Ynab.Http;
using YnabCli.Commands.Factories;
using YnabCli.Commands.Handlers;
using YnabCli.Database;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.ViewModelBuilders;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.Commands.Reporting.RecurringTransactions;

public class RecurringTransactionsCommandHandler : CommandHandler, ICommandHandler<RecurringTransactionsCommand>
{
    private readonly BudgetsClientFactory _budgetsClientFactory;
    private readonly TransactionMemoOccurrenceViewModelBuilder _viewModelBuilder;

    public RecurringTransactionsCommandHandler(
        BudgetsClientFactory budgetsClientFactory,
        TransactionMemoOccurrenceViewModelBuilder viewModelBuilder)
    {
        _budgetsClientFactory = budgetsClientFactory;
        _viewModelBuilder = viewModelBuilder;
    }

    private const int DefaultMinimumOccurrences = 2;
    

    public async Task<ConsoleTable> Handle(RecurringTransactionsCommand command, CancellationToken cancellationToken)
    {
        var _budgetsClient = await _budgetsClientFactory.Create();
        var budgets = await _budgetsClient.GetBudgets();
        var budget =  budgets.First();
        
        var transactions = await GetTransactions(budget, command);

        var aggregator = new TransactionMemoOccurrenceAggregator(transactions);

        _viewModelBuilder
            .AddAggregator(aggregator)
            .AddColumnNames(TransactionMemoOccurrenceViewModel.GetColumnNames());

        var viewModel = _viewModelBuilder
            .AddMinimumOccurrencesFilter(command.MinimumOccurrences ?? DefaultMinimumOccurrences)
            .AddSortOrder(ViewModelSortOrder.Descending)
            .Build();

        return Compile(viewModel);
    }
    
    private async Task<IEnumerable<Transaction>> GetTransactions(Budget budget, RecurringTransactionsCommand command)
    {
        var transactions = await budget.GetTransactions();

        if (command.From.HasValue)
        {
            transactions = transactions.FilterFrom(command.From.Value);
        }

        if (command.To.HasValue)
        {
            transactions = transactions.FilterTo(command.To.Value);
        }

        if (command.PayeeName != null)
        {
            transactions = transactions.FilterByPayeeName(command.PayeeName);
        }
        
        return transactions;
    }
}