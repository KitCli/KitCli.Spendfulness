namespace Ynab.Collections;

public class TransactionsByPayeeName
{
    public string PayeeName { get; set; }
    public IEnumerable<Transaction> Transactions { get; set; }
}