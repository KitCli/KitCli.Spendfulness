using Cli.Commands.Abstractions.Extensions;
using Cli.Instructions.Abstractions;

namespace Cli.Commands.Abstractions;

public class CliCommandMap
{
    private readonly List<string> _nextCommands = new();
    
    public CliCommandMap Next<TNextCommand>() where TNextCommand : CliCommand
    {
        var nextCommandName = typeof(TNextCommand)
            .GetCommandName()
            .ToLowerSplitString(CliInstructionConstants.DefaultCommandNameSeparator);

        _nextCommands.Add(nextCommandName);

        return this;
    }

    public bool CanMoveTo(string nextCommandNAme) => _nextCommands.Contains(nextCommandNAme);
}