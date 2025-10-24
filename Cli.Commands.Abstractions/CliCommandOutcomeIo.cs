
namespace Cli.Commands.Abstractions;

public class CliCommandOutcomeIo : CliIo
{
    public void Say(CliCommandOutcome outcome)
    {
        switch (outcome)
        {
            case CliCommandTableOutcome tableOutcome:
                Say(tableOutcome);
                break;
            case CliCommandOutputOutcome outputOutcome:
                Say(outputOutcome);
                break;
            case CliCommandNotFoundOutcome nothingOutcome:
                Say(nothingOutcome);
                break;
            case CliCommandNothingOutcome:
                break;
            default:
                throw new UnknownCliCommandOutcomeException(
                    $"{outcome.Kind} outcomes not supported");
        }
    }
    public void Say(CliCommandTableOutcome tableOutcome)
        => Say(tableOutcome.Table.ToString());
    
    public void Say(CliCommandOutputOutcome outputOutcome)
        => Say(outputOutcome.Output);
    
    public void Say(CliCommandNothingOutcome nothingOutcome)
        => Say("Command not found.");
}