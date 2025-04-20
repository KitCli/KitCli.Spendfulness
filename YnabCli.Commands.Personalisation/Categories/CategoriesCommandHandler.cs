using ConsoleTables;
using YnabCli.Aggregation.Aggregator.ListAggregators;
using YnabCli.Commands.Handlers;
using YnabCli.Database;
using YnabCli.ViewModels.ViewModelBuilders;

namespace YnabCli.Commands.Personalisation.Categories;

public class CategoriesCommandHandler: CommandHandler, ICommandHandler<CategoriesCommand>
{
    private readonly ConfiguredBudgetClient _configuredBudgetClient;
    private readonly CategoryViewModelBuilder _categoryViewModelBuilder;

    public CategoriesCommandHandler(ConfiguredBudgetClient configuredBudgetClient, CategoryViewModelBuilder categoryViewModelBuilder)
    {
        _configuredBudgetClient = configuredBudgetClient;
        _categoryViewModelBuilder = categoryViewModelBuilder;
    }

    public async Task<ConsoleTable> Handle(CategoriesCommand request, CancellationToken cancellationToken)
    {
        var budget = await _configuredBudgetClient.GetDefaultBudget();
        
        var categoryGroups = await budget.GetCategoryGroups();
        
        var aggregator = new CategoryAggregator(categoryGroups);

        var viewModel = _categoryViewModelBuilder
            .WithAggregator(aggregator)
            .Build();
        
        return Compile(viewModel);
    }
}