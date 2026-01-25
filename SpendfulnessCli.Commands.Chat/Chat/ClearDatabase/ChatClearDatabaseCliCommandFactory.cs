using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Artefacts;
using Cli.Commands.Abstractions.Factories;
using Cli.Instructions.Abstractions;

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