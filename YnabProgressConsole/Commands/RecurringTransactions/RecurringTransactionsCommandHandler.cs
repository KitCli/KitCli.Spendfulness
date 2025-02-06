using ConsoleTables;
using Ynab.Clients;
using Ynab.Extensions;
using YnabProgress.ViewModels;

namespace YnabProgressConsole.Commands.RecurringTransactions;

public class RecurringTransactionsCommandHandler(BudgetsClient budgetsClient)
    : BaseCommandHandler, ICommandHandler<RecurringTransactionsCommand>
{
    public async Task<ConsoleTable> Handle(RecurringTransactionsCommand request, CancellationToken cancellationToken)
    {
        var budgets = await budgetsClient.GetBudgets();
        
        // TODO: Add support for selecting a budget if you ever do a settings feture.
        var budget =  budgets.First();
        
        var allTransactions = await budget.GetTransactions();

        var transactions = allTransactions
            .GroupByPayeeName()
            .GroupbyMemoOccurence();

        // TODO: This needs to render a proper view model.
        return Compile(new ViewModel());
    }
}


