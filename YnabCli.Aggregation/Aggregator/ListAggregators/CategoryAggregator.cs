using Ynab;
using YnabCli.Aggregation.Aggregates;

namespace YnabCli.Aggregation.Aggregator.ListAggregators;

public class CategoryAggregator(IEnumerable<CategoryGroup> categoryGroups) 
    : ListAggregator<CategoryAggregate>(categoryGroups)
{
    protected override IEnumerable<CategoryAggregate> ListAggregate()
        => categoryGroups
            .SelectMany(categoryGroup => categoryGroup.Categories)
            .Select(category => new CategoryAggregate(category.Id, category.Name));
}