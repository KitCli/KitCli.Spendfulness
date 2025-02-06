namespace Ynab.Collections;

public class TransactionsByMemoOccurenceByPayeeName
{
    public string PayeeName { get; set; }
    public IEnumerable<TransactionsByMemoOccurence> TransactionsByMemoOccurences { get; set; }
}