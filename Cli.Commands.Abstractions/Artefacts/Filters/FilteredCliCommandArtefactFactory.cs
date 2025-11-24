using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Outcomes.Final;

namespace Cli.Commands.Abstractions.Artefacts.Filters;

public class FilteredCliCommandArtefactFactory : ICliCommandArtefactFactory
{
    public bool For(CliCommandOutcome outcome) => outcome is FilteredCliCommandOutcome;

    public CliCommandArtefact Create(CliCommandOutcome outcome)
    {
        var filteredOutcome = (FilteredCliCommandOutcome)outcome;
        return new FilteredCliCommandArtefact(filteredOutcome.AppliedFilters);
    }
}