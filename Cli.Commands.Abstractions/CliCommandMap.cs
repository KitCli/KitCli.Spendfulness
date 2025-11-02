using Cli.Commands.Abstractions.Extensions;
using Cli.Instructions.Abstractions;

namespace Cli.Commands.Abstractions;

public class CliCommandMap
{
    private readonly List<string> _nextCommands = new();
    
    public CliCommandMap Next<TNextCommand>() where TNextCommand : CliCommand
    {
        var commandName = typeof(TNextCommand).GetCommandName();

        var commandInstruction = commandName.ToLowerSplitString(CliInstructionConstants.DefaultCommandNameSeparator);
        var commandShorthandInstruction = commandName.ToLowerTitleCharacters();

        _nextCommands.AddRange(commandInstruction, commandShorthandInstruction);

        return this;
    }

    public bool CanMoveTo(string nextCommandNAme) => _nextCommands.Contains(nextCommandNAme);
}