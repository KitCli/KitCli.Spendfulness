using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;
using Microsoft.Azure.Cosmos;

namespace SpendfulnessCli.Commands.Chat.Chat.ClearDatabase;

public class ChatClearDatabaseCliCommandHandler : CliCommandHandler, ICliCommandHandler<ChatClearDatabaseCliCommand>
{
    private readonly CosmosClient _cosmosClient;

    public ChatClearDatabaseCliCommandHandler(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    public async Task<CliCommandOutcome[]> Handle(ChatClearDatabaseCliCommand request, CancellationToken cancellationToken)
    {
        var database = _cosmosClient.GetDatabase("chatdb");
        
        var container = database.GetContainer("transactions");
        
        await container.DeleteContainerAsync(cancellationToken: cancellationToken);
        
        await database.CreateContainerIfNotExistsAsync("transactions", "/id", cancellationToken: cancellationToken);
        
        return OutcomeAs("Chat database cleared successfully.");
    }
}