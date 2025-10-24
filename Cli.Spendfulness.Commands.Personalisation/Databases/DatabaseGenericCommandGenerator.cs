using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Spendfulness.Commands.Personalisation.Databases.Create;

namespace Cli.Spendfulness.Commands.Personalisation.Databases;

public class DatabaseGenericCommandGenerator : ICommandGenerator<DatabaseCliCommand>
{
    public ICliCommand Generate(CliInstruction instruction) =>
        instruction.SubInstructionName switch
        {
            DatabaseCliCommand.SubCommandNames.Create => new DatabaseCreateCliCommand(),
            _ => new DatabaseCliCommand()
        };
}