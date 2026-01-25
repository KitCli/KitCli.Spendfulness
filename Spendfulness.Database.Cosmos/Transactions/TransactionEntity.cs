namespace Spendfulness.Database.Cosmos.Transactions;

public class TransactionEntity
{
    public string Id { get; set; }
    public string PayeeName { get; set; }
    public string CategoryName { get; set; }
    public string? Memo { get; set; }
    public decimal Amount { get; set; }
}