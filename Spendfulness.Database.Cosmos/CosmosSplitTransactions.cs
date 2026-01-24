namespace Spendfulness.Database.Cosmos;

public class SplitTransactionEntity
{
    public string Id { get; set; }
    public DateTime Occured { get; set; }
    public string? Memo { get; set; }
    public decimal Amount { get; set; }
    public Guid? PayeeId { get; set; }
    public string PayeeName { get; set; }
    public Guid? CategoryId { get; set; }
    public string CategoryName { get; set; }
    public bool IsTransfer { get; set; }
    public Guid? AccountId { get; set; }
}