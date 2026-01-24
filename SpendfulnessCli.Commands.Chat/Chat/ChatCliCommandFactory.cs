using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;
using Cli.Instructions.Arguments;

namespace SpendfulnessCli.Commands.Chat.Chat;

public class ChatCliCommandFactory : ICliCommandFactory<ChatCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => string.IsNullOrEmpty(instruction.SubInstructionName);
    
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        var promptArgument = instruction
            .Arguments
            .OfRequiredType<string>(ChatCliCommand.ArgumentNames.Prompt);

        return new ChatCliCommand(promptArgument.ArgumentValue);
    }
}