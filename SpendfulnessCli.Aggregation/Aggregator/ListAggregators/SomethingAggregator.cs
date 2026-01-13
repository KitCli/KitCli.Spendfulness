using SpendfulnessCli.Aggregation.Aggregates;
using Ynab;
using Ynab.Collections;
using Ynab.Extensions;

namespace SpendfulnessCli.Aggregation.Aggregator.ListAggregators;

public class SomethingAggregator(
    BudgetYears budgetYears,
    IEnumerable<Transaction> transactions,
    IEnumerable<CategoryGroup> categoryGroups)
    : YnabListAggregator<SomeAggregateCollection>(transactions, categoryGroups)
{
    protected override IEnumerable<SomeAggregateCollection> GenerateAggregate()
    {
        var splitTransactions = Transactions
            .Where(transaction => transaction.SplitTransactions.Any())
            .SelectMany(transaction => transaction.SplitTransactions);
        
        var mergedTransactions = Transactions
            .Where(transaction => !transaction.SplitTransactions.Any())
            .Concat(splitTransactions);
        
        var transactionsByCategoryByYears = mergedTransactions
            .GroupByCategory()
            .GroupByYear()
            .ToList();

        var spendingCategoryGroups = CategoryGroups
            .FilterToSpendingCategories();
        
        // --- Below is an aggregation process.---
        var someAggregateCollections = new List<SomeAggregateCollection>();

        // Every group should show.
        foreach (var categoryGroup in spendingCategoryGroups)
        {
            var someAggregates = new List<SomeAggregate>();

            foreach (var category in categoryGroup.Categories)
            {
                var byYears = new List<SplitTransactionsByYear>();
                
                var categoryTransactions = transactionsByCategoryByYears
                    .FirstOrDefault(tcy => tcy.CategoryId == category.Id);
                
                foreach (var year in budgetYears.All)
                {
                    var transactionByYear = categoryTransactions?.TransactionsByYear.FirstOrDefault(tby => tby.Year == year);
                    
                    var transactionsInYear = transactionByYear ?? new SplitTransactionsByYear(year, new List<SplitTransactions>());
                    
                    byYears.Add(transactionsInYear);
                }
                
                var agg = new SomeAggregate
                {
                    CategoryName = category.Name,
                    TransactionsByYears = byYears
                };
                
                someAggregates.Add(agg);
            }
            
            var collection = new SomeAggregateCollection
            {
                CategoryGroupName = categoryGroup.Name,
                Aggregates = someAggregates
            };
            
            someAggregateCollections.Add(collection);
        }

        return someAggregateCollections;
    }
}