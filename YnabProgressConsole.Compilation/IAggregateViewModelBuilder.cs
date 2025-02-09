using Ynab.Aggregates;

namespace YnabProgressConsole.Compilation;

public interface IAggregateViewModelBuilder<in TAggregation, TAggregate>  : IViewModelBuilder 
    where TAggregation : YnabAggregation<TAggregate>
    where TAggregate : class
{
    IAggregateViewModelBuilder<TAggregation, TAggregate> AddAggregation(TAggregation aggregate);
}