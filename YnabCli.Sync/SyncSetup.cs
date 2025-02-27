using Microsoft.Extensions.DependencyInjection;
using Ynab.Extensions;
using YnabCli.Database;
using YnabCli.Sync.Synchronisers;

namespace YnabCli.Sync;

public static class SyncSetup
{
    public static void Setup(this IServiceCollection serviceCollection)
    {
        // Dependencies
        serviceCollection
            .AddYnab()
            .AddDb();
        
        // Sync-related
        serviceCollection
            .AddSingleton<BudgetGetter>()
            .AddHostedService<DatabaseSynchroniser>() // Ensure db is created.
            .AddHostedService<CommitmentSynchroniser>();
    }
}