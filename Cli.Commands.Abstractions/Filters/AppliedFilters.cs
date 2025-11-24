namespace Cli.Commands.Abstractions.Filters;

public class AppliedFilter
{
    public string FilterFieldName { get; }
    public string FilterName { get; }
    
    public AppliedFilter(
        string filterFieldName,
        string filterName)
    {
        FilterFieldName = filterFieldName;
        FilterName = filterName;
    }
}