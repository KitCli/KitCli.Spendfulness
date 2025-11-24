using Cli.Commands.Abstractions.Filters;

namespace Cli.Commands.Abstractions.Artefacts.Filters;

public class FilteredCliCommandArtefact : CliCommandArtefact
{
    public List<AppliedFilter> AppliedFilters { get; }
    
    public FilteredCliCommandArtefact(List<AppliedFilter> appliedFilters)
    {
        AppliedFilters = appliedFilters;
    }
}