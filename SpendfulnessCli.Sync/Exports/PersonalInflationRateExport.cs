using Microsoft.Extensions.Hosting;
using Spendfulness.Database;
using SpendfulnessCli.Aggregation.Aggregates;
using SpendfulnessCli.Aggregation.Aggregator.ListAggregators;
using Ynab;

namespace SpendfulnessCli.Sync.Exports;

public class PersonalInflationRateExport(SpendfulnessBudgetClient spendfulnessBudgetClient) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var defaultBudget = await spendfulnessBudgetClient.GetDefaultBudget();
        
        var transactions = await defaultBudget.GetTransactions();
        var categoryGroups = await defaultBudget.GetCategoryGroups();
        
        var years = defaultBudget.GetYears();

        var aggregates = new SomethingAggregator(years, transactions, categoryGroups)
            .Aggregate();

        // await WriteCsv(years, aggregates);
        
        Console.WriteLine("Should have been written");
    }
}












