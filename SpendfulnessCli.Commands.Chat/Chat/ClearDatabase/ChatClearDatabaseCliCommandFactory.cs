using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Chat.Chat.ClearDatabase;

public class ChatClearDatabaseCliCommandFactory : ICliCommandFactory<ChatCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        return instruction.SubInstructionName == ChatCliCommand.SubCommandNames.ClearDatabase;
    }

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
    {
        return new ChatClearDatabaseCliCommand();
    }
}