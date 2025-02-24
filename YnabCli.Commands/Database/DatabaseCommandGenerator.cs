using YnabCli.Commands.Database.Create;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Database;

public class DatabaseCommandGenerator : ICommandGenerator, ITypedCommandGenerator<DatabaseCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
    {
        if (subCommandName == DatabaseCommand.SubCommandNames.Create)
        {
            return new DatabaseCreateCommand();
        }
        
        return new DatabaseCommand();
    }
}