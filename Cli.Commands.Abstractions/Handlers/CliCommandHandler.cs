using Cli.Abstractions;
using Cli.Commands.Abstractions.Filters;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Outcomes.Final;

namespace Cli.Commands.Abstractions.Handlers;

public abstract class CliCommandHandler
{
    protected static CliCommandOutcome[] OutcomeAs()
        => [new CliCommandNothingOutcome()];
    
    protected static Task<CliCommandOutcome[]> AsyncOutcomeAs()
        => Task.FromResult(OutcomeAs());
    
    protected static CliCommandOutcome[] OutcomeAs(CliTable cliTable)
        => [new CliCommandTableOutcome(cliTable)];

    protected static CliCommandOutcome[] OutcomeAs(string message)
        => [new CliCommandOutputOutcome(message)];

    protected static Task<CliCommandOutcome[]> AsyncOutcomeAs(string message)
        => Task.FromResult(OutcomeAs(message));
    
    protected static CliCommandOutcome[] OutcomeAs(List<AppliedFilter> appliedFilters)
        => [new FilteredCliCommandOutcome(appliedFilters)];
    
    protected static Task<CliCommandOutcome[]> AsyncOutcomeAs(List<AppliedFilter> appliedFilters)
        => Task.FromResult(OutcomeAs(appliedFilters));
}