namespace Ynab.Collections;

public class TransactionsByMemoOccurence
{
    public required string? Memo { get; set; }
    public required int MemoOccurence { get; set; }
    public required IEnumerable<Transaction> Transactions { get; set; }
}