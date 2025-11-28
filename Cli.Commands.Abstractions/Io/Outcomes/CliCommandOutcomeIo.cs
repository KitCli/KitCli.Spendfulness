using Cli.Commands.Abstractions.Outcomes;
using Cli.Commands.Abstractions.Outcomes.Final;

namespace Cli.Commands.Abstractions.Io.Outcomes;

public class CliCommandOutcomeIo : CliIo, ICliCommandOutcomeIo
{
    public void Say(CliCommandOutcome[] outcomes)
    {
        foreach (var outcome in outcomes)
        {
            Say(outcome);
        }
    }

    private void Say(CliCommandOutcome outcome)
    {
        switch (outcome)
        {
            case CliCommandTableOutcome tableOutcome:
                Say(tableOutcome.Table.ToString());
                break;
            case CliCommandOutputOutcome outputOutcome:
                Say(outputOutcome.Output);
                break;
            case CliCommandNotFoundOutcome nothingOutcome:
                Say(string.Empty);
                break;
            case CliCommandExceptionOutcome exceptionOutcome:
                Say(exceptionOutcome.Exception.ToString());
                break;
        }
    }
}