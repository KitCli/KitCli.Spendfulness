using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;
using YnabCli.Commands.Generators;
using YnabCli.Commands.Personalisation.Databases.Create;

namespace YnabCli.Commands.Personalisation.Databases;

public class DatabaseGenericCommandGenerator : ICommandGenerator<DatabaseCommand>
{
    public ICommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        if (subCommandName == DatabaseCommand.SubCommandNames.Create)
        {
            return new DatabaseCreateCommand();
        }
        
        return new DatabaseCommand();
    }
}