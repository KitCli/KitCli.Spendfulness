namespace Spendfulness.Database.Cosmos;

public class TransactionEntity : SplitTransactionEntity
{
    public string? FlagName { get; set; }
    public string? FlagColour { get; set; }
    public IEnumerable<SplitTransactionEntity>? SplitTransactions { get; set; }
}