namespace Spendfulness.Database.Cosmos;

public class CosmosSettings 
{
    public string AccountEndpoint { get; set; } 
    public string AccountKey { get; set; }
    public int PerSecondRuLimit { get; set; }
    public int PerTransactionCreateRuCost { get; set; }
}