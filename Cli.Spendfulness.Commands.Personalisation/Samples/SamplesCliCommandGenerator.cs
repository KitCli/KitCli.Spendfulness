using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using Cli.Spendfulness.Commands.Personalisation.Samples.Matches;

namespace Cli.Spendfulness.Commands.Personalisation.Samples;

public class SamplesCliCommandGenerator : ICliCommandGenerator<SamplesCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction)
        => instruction.SubInstructionName switch
        {
            SamplesCliCommand.SubCommandNames.Matches => GetMatchesCommand(instruction.Arguments),
            _ => new SamplesCliCommand()
        };
    
    private SamplesMatchesCliCommand GetMatchesCommand(List<CliInstructionArgument> arguments)
    {
        var trnasactionIdArgument = arguments
            .OfRequiredStringFrom<Guid, string>(SamplesMatchesCliCommand.ArgumentNames.TransactionId);

        return new SamplesMatchesCliCommand
        {
            TransactionId = trnasactionIdArgument.ArgumentValue
        };
    }

}