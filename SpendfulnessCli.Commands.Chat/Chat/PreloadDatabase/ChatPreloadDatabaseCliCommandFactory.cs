using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Chat.Chat.PreloadDatabase;

public class ChatPreloadDatabaseCliCommandFactory : ICliCommandFactory<ChatCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == ChatCliCommand.SubCommandNames.PreloadDatabase;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new ChatPreloadDatabaseCliCommand();
}