namespace Ynab.Collections;

public class TransactionsByMemoOccurrenceByPayeeName
{
    public required string PayeeName { get; set; }
    public required IEnumerable<TransactionsByMemoOccurence> TransactionsByMemoOccurences { get; set; }
}