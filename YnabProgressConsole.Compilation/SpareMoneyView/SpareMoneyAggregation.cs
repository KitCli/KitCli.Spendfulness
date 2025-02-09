using Ynab.Aggregates;

namespace YnabProgressConsole.Compilation.SpareMoneyView;

public class SpareMoneyAggregation(IEnumerable<AccountBalanceAggregate> aggregation, decimal amountToDeduct)
    : YnabAggregation<AccountBalanceAggregate>(aggregation)
{
    public decimal AmountToDeduct { get; set; } = amountToDeduct;
}