using Microsoft.Extensions.Hosting;
using YnabCli.Database;

namespace YnabCli.Sync.Synchronisers;

public class DatabaseSynchroniser(YnabCliDbContext dbContext) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await dbContext.Database.EnsureCreatedAsync(stoppingToken);
    }
}