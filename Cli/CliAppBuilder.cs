using Microsoft.Extensions.DependencyInjection;

namespace Cli;

public class CliAppBuilder
{
    private readonly ServiceCollection _services = [];

    public CliAppBuilder WithCli<TCliApp>() where TCliApp : CliApp
    {
        _services.AddCli<TCliApp>();
        
        return this;
    }

    public CliAppBuilder WithCustomServices(Func<ServiceCollection, IServiceCollection> configureServices)
    {
        configureServices(_services);
        
        return this;
    }

    public async Task Run()
    {
        var serviceProvider = _services.BuildServiceProvider();
        
        var cliApp = serviceProvider.GetRequiredService<CliApp>();
        
        await cliApp.Run();
    }
}