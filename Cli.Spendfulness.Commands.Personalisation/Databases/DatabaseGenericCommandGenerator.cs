using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using Cli.Spendfulness.Commands.Personalisation.Databases.Create;

namespace Cli.Spendfulness.Commands.Personalisation.Databases;

public class DatabaseGenericCommandGenerator : ICommandGenerator<DatabaseCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        if (subCommandName == DatabaseCliCommand.SubCommandNames.Create)
        {
            return new DatabaseCreateCliCommand();
        }
        
        return new DatabaseCliCommand();
    }
}