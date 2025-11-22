using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Personalisation.Users.Create;

public class CreateUserCliCommandFactory : ICliCommandFactory<UserCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == UserCliCommand.SubCommandNames.Create;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var userNameArgument = instruction
            .Arguments
            .OfRequiredType<string>(CreateUserCliCommand.ArgumentNames.UserName);

        return new CreateUserCliCommand
        {
            UserName = userNameArgument.ArgumentValue
        };
    }
}