using Ynab.Collections;

namespace Spendfulness.Aggregation.Aggregates;

public record TransactionByYearsByCategoryAggregate(string CategoryName, IEnumerable<SplitTransactionsByYear> TransactionsByYears);