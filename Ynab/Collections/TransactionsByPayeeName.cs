namespace Ynab.Collections;

public class TransactionsByPayeeName
{
    public required string PayeeName { get; set; }
    public required IEnumerable<Transaction> Transactions { get; set; }
}