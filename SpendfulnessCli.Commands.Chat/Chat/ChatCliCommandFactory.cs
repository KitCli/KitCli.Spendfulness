using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;
using KitCli.Instructions.Arguments;

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