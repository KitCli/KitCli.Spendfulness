using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace YnabProgress.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddYnabProgress(this IServiceCollection serviceCollection)
        => serviceCollection.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
}