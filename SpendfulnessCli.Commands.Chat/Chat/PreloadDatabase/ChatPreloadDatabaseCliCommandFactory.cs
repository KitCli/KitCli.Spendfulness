using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Chat.Chat.PreloadDatabase;

public class ChatPreloadDatabaseCliCommandFactory : ICliCommandFactory<ChatCliCommand>
{
    public bool CanCreateWhen(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => instruction.SubInstructionName == ChatCliCommand.SubCommandNames.PreloadDatabase;

    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new ChatPreloadDatabaseCliCommand();
}