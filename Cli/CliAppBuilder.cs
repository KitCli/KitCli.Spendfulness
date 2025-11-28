using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cli;

public class CliAppBuilder
{
    private readonly ServiceCollection _services = [];
    private IConfigurationBuilder _configurationBuilder;
    private IConfigurationRoot? _configuration = null;
    
    public CliAppBuilder WithCli<TCliApp>() where TCliApp : CliApp
    {
        _services.AddCli<TCliApp>();

        _services.AddOptions();
        _configurationBuilder = new ConfigurationBuilder();
        
        return this;
    }

    public CliAppBuilder WithUserSecretsSettings()
    {
        var cliAppServiceDescriptor = _services
            .FirstOrDefault(x => x.ServiceType == typeof(CliApp));

        if (cliAppServiceDescriptor is null)
        {
            throw new Exception("CliApp must be registered before calling WithUserSecretsSettings");
        }
        
        _configurationBuilder
            .AddUserSecrets(cliAppServiceDescriptor.ImplementationType!.Assembly);

        return this;
    }

    public CliAppBuilder WithJsonSettings(string fileName)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        
        _configurationBuilder
            .SetBasePath(currentDirectory)
            .AddJsonFile(fileName, optional: true, reloadOnChange: true);

        return this;
    }
    
    public CliAppBuilder WithSettings<TSettings>() where TSettings : class
    {
        var configurationName = typeof(TSettings)
            .Name
            .Replace("Settings", string.Empty);
        
        var configuration = _configuration ??= _configurationBuilder.Build();
        
        var section = configuration.GetSection(configurationName);
        
        _services.Configure<TSettings>(section);

        return this;
    }

    public CliAppBuilder WithCustomServices(Func<ServiceCollection, IServiceCollection> configureServices)
    {
        configureServices(_services);
        
        return this;
    }

    public CliApp Build()
    {
        var serviceProvider = _services.BuildServiceProvider();
        
        return serviceProvider.GetRequiredService<CliApp>();
    }
}