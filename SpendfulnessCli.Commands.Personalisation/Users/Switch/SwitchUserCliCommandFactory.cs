using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;
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