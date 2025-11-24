using Cli.Commands.Abstractions.Filters;

namespace Cli.Commands.Abstractions.Outcomes.Final;

public class FilteredCliCommandOutcome : CliCommandOutcome
{
    public FilteredCliCommandOutcome(List<AppliedFilter> appliedFilters) : base(CliCommandOutcomeKind.Reusable)
    {
        AppliedFilters = appliedFilters;
    }

    public List<AppliedFilter> AppliedFilters { get; }
}