namespace Ynab.Collections;

public class TransactionsByMemoOccurence
{
    public string Memo { get; set; }
    public int MemoOccurence { get; set; }
    public IEnumerable<Transaction> Transactions { get; set; }
}