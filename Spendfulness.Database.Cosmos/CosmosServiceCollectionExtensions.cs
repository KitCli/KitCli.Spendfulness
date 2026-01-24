using Azure;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace Spendfulness.Database.Cosmos;

public static class CosmosServiceCollectionExtensions
{
    public static IServiceCollection AddSpendfulnessCosmos(this IServiceCollection services, CosmosSettings settings)
    {
        var credential = new AzureKeyCredential(settings.AccountKey);
        
        var client = new CosmosClient(settings.AccountEndpoint, credential, new CosmosClientOptions
        {
            SerializerOptions = new CosmosSerializationOptions
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            }
        });
        
        services.AddSingleton(client);
        
        return services;
    }
}