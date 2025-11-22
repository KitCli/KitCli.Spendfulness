using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using SpendfulnessCli.Commands.Personalisation.Users.Create;

namespace SpendfulnessCli.Commands.Personalisation.Users.Switch;

public class SwitchUserCliCommandFactory : ICliCommandFactory<UserCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == UserCliCommand.SubCommandNames.Switch;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var userNameArgument = instruction
            .Arguments
            .OfRequiredType<string>(CreateUserCliCommand.ArgumentNames.UserName);

        return new UserSwitchCliCommand
        {
            UserName = userNameArgument.ArgumentValue
        };
    }
}