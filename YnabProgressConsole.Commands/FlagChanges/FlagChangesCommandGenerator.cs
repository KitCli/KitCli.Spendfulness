using YnabProgressConsole.Instructions.InstructionArguments;

namespace YnabProgressConsole.Commands.FlagChanges;

public class FlagChangesCommandGenerator : ICommandGenerator, ITypedCommandGenerator<FlagChangesCommand>
{
    public ICommand Generate(List<InstructionArgument> arguments)
    {
        var from = arguments.OfType<DateOnly>(FlagChangesCommand.ArgumentNames.From);

        return new FlagChangesCommand
        {
            From = from?.ArgumentValue
        };
    }
}