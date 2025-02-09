using Ynab.Aggregates;

namespace YnabProgressConsole.Compilation.SpareMoneyView;

public static class YnabAggregationExtensions
{
    public static SpareMoneyAggregation IncludeAmountToIgnore(
        this YnabAggregation<AccountBalanceAggregate> aggregation, decimal amountToIgnore)
            => new(aggregation.Aggregation, amountToIgnore);
}