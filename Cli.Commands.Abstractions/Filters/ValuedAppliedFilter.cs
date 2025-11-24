namespace Cli.Commands.Abstractions.Filters;

public class ValuedAppliedFilter<TFilterValue> : AppliedFilter
    where TFilterValue : notnull
{
    public TFilterValue FilterValue { get; }
    
    public ValuedAppliedFilter(
        string filterFieldName,
        string filterName,
        TFilterValue filterValue)
        : base(filterFieldName, filterName)
    {
        FilterValue = filterValue;
    }
}