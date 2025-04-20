using YnabCli.Commands.Generators;
using YnabCli.Instructions.Arguments;

namespace YnabCli.Commands.Personalisation.Categories;

public class CategoriesCommandGenerator : ICommandGenerator, ITypedCommandGenerator<CategoriesCommand>
{
    public ICommand Generate(string? subCommandName, List<InstructionArgument> arguments)
    {
        return new CategoriesCommand();
    }
}