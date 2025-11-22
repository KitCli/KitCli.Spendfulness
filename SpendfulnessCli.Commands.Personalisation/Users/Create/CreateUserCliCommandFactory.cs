using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Attributes;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;
using SpendfulnessCli.Commands.Personalisation.Users.Create;

namespace SpendfulnessCli.Commands.Personalisation.Users;

[FactoryFor(typeof(UserCliCommand))]
public class CreateUserCliCommandFactory : ICliCommandFactory<CreateUserCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> properties)
        => instruction.SubInstructionName == UserCliCommand.SubCommandNames.Create;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> properties)
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