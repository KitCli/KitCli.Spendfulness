using KitCli.Commands.Abstractions;
using KitCli.Commands.Abstractions.Artefacts;
using KitCli.Commands.Abstractions.Factories;
using KitCli.Instructions.Abstractions;

namespace SpendfulnessCli.Commands.Personalisation.Categories;

public class CategoriesGenericCliCommandFactory : ICliCommandFactory<CategoriesGenericCliCommandFactory>
{
    public CliCommand Create(CliInstruction instruction, List<CliCommandArtefact> artefacts)
        => new CategoriesCliCommand();
}