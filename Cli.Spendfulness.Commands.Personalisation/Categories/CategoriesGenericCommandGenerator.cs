using Cli.Commands.Abstractions;
using Cli.Instructions.Abstractions;

namespace Cli.Spendfulness.Commands.Personalisation.Categories;

public class CategoriesGenericCommandGenerator : ICommandGenerator<CategoriesCliCommand>
{
    public ICliCommand Generate(string? subCommandName, List<ConsoleInstructionArgument> arguments)
    {
        return new CategoriesCliCommand();
    }
}