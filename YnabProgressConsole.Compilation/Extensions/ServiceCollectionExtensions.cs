using Microsoft.Extensions.DependencyInjection;
using Ynab.Collections;
using YnabProgressConsole.Compilation.Evaluators;
using YnabProgressConsole.Compilation.ViewModelBuilders;

namespace YnabProgressConsole.Compilation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsoleCompilation(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddKeyedSingleton<IGroupViewModelBuilder<TransactionsByMemoOccurrenceByPayeeName>,
                TransactionsByMemoOccurrenceByPayeeNameGroupViewModelBuilder>(
                typeof(TransactionsByMemoOccurrenceByPayeeName))
            .AddKeyedSingleton<IGroupViewModelBuilder<AmountByYear>,
                AmountByYearGroupViewModelBuilder>(typeof(AmountByYear))
            .AddKeyedSingleton<IEvaluationViewModelBuilder<CategoryDeductedBalanceEvaluator, decimal>,
                CompanyDeductedBalanceEvaluator>(typeof(CategoryDeductedBalanceEvaluator));
}