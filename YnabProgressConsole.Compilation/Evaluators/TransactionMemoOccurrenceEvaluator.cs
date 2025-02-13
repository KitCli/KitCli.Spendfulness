using Ynab;
using Ynab.Extensions;
using YnabProgressConsole.Compilation.Aggregates;
using YnabProgressConsole.Compilation.Extensions;

namespace YnabProgressConsole.Compilation.Evaluators;

public class TransactionMemoOccurrenceEvaluator : YnabEvaluator<IEnumerable<TransactionMemoOccurrenceAggregate>>
{
    public TransactionMemoOccurrenceEvaluator(
        IEnumerable<Transaction>? transactions = null)
        : base(
            null,
            null,
            null,
            null,
            transactions)
    {}
    
    

    public override IEnumerable<TransactionMemoOccurrenceAggregate> Evaluate() =>
        Transactions
            .FilterToSpending()
            .GroupByPayeeName()
            .GroupByMemoOccurence()
            .Aggregate();
}