using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Reporting.FlagChanges;

public class FlagChangesCliCommandFactory : ICliCommandFactory<FlagChangesCliCommand>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var from = instruction
            .Arguments
            .OfType<DateOnly>(FlagChangesCliCommand.ArgumentNames.From);
        
        var to = instruction.
            Arguments
            .OfType<DateOnly>(FlagChangesCliCommand.ArgumentNames.To);

        return new FlagChangesCliCommand
        {
            From = from?.ArgumentValue,
            To = to?.ArgumentValue
        };
    }
}