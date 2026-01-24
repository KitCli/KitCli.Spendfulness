using Cli.Abstractions.Io;
using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;
using Microsoft.Azure.Cosmos;
using Spendfulness.Database.Cosmos;
using Spendfulness.Database.Sqlite;
using Ynab;

namespace SpendfulnessCli.Commands.Chat.Chat.PreloadDatabase;

public class ChatPreloadDatabaseCliCommandHandler : CliCommandHandler, ICliCommandHandler<ChatPreloadDatabaseCliCommand>
{
    private readonly CosmosClient _cosmosClient;
    private readonly SpendfulnessBudgetClient _spendfulnessBudgetClient;
    private readonly ICliIo _io;
    
    private const int PerRequestRuCost = 9;
    private const int PerSecondRuLimit = 1000;
    
    // Each request costs 9 RUs, so we can do 111 requests per second without exceeding the 1000 RU limit
    private const int MaxItemsPerSecond = PerSecondRuLimit / PerRequestRuCost;

    public ChatPreloadDatabaseCliCommandHandler(CosmosClient cosmosClient, SpendfulnessBudgetClient spendfulnessBudgetClient, ICliIo io)
    {
        _cosmosClient = cosmosClient;
        _spendfulnessBudgetClient = spendfulnessBudgetClient;
        _io = io;
    }

    public async Task<CliCommandOutcome[]> Handle(ChatPreloadDatabaseCliCommand command, CancellationToken cancellationToken)
    {
        var transactionContainer = GetCosmosTransactionContainer();
        
        var ynabTransactions = await GetYnabTransactions();
        
        var missingYnabTransactions = await GetMissingTransactions(transactionContainer, ynabTransactions, cancellationToken);
        
        for (var nextBatchStartingPoint = 0; nextBatchStartingPoint < missingYnabTransactions.Count; nextBatchStartingPoint += MaxItemsPerSecond)
        {
            var initialItemNumber = nextBatchStartingPoint;
            
            var createItemTasks = missingYnabTransactions
                .Skip(nextBatchStartingPoint)
                .Take(MaxItemsPerSecond)
                .Select(t => t.ToCosmosTransaction())
                .Select((transaction, currentIndex) => CreateItem(initialItemNumber + currentIndex + 1, transactionContainer, transaction, cancellationToken));
            
            await Task.WhenAll(createItemTasks);

            // RU limit is 1000 per second
            await Task.Delay(1000, cancellationToken);
        }
        
        return OutcomeAs($"{missingYnabTransactions.Count} Transactions Preloaded Into Cosmos");
    }
    
    private Container GetCosmosTransactionContainer()
    {
        var chatDb = _cosmosClient.GetDatabase("chatdb");
        return chatDb.GetContainer("transactions");
    }

    private async Task<List<Transaction>> GetYnabTransactions()
    {
        var budget = await _spendfulnessBudgetClient.GetDefaultBudget();
        var transactions =  await budget.GetTransactions();
        return transactions.ToList();
    }

    private async Task<List<Transaction>> GetMissingTransactions(Container transactionContainer, List<Transaction> ynabTransactions, CancellationToken cancellationToken)
    {
        var ynabTransactionIdentifiers = ynabTransactions
            .Select(ynabTransaction => ynabTransaction.GetIdentifier())
            .ToList();
        
        var feedResponse = await transactionContainer.ReadManyItemsAsync<TransactionEntity>(
            ynabTransactionIdentifiers, null, cancellationToken);

        var existingItemIds = feedResponse.Select(cosmosTransaction => cosmosTransaction.Id).ToList();
        
        var missingTransactionIds =  ynabTransactionIdentifiers
            .Where(identifier => !existingItemIds.Contains(identifier.Id))
            .Select(identifier => identifier.Id)
            .ToList();
        
        return ynabTransactions
            .Where(transaction => missingTransactionIds.Contains(transaction.Id))
            .ToList();
    }

    private async Task CreateItem(int itemNumber, Container container, TransactionEntity transactionEntity, CancellationToken cancellationToken)
    {
        _io.Say($"Started Creating Item Cosmos Transaction #{itemNumber} (PartitionKey: {transactionEntity.Id})");
        
        var itemResponse = await container.CreateItemAsync(
            transactionEntity,
            new PartitionKey(transactionEntity.Id),
            null,
            cancellationToken);
            
        _io.Say($"Finished Creating Item Cosmos Transaction #{itemNumber} (PartitionKey: {itemResponse.Resource.Id})");
    }
}