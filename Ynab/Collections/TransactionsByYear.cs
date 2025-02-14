namespace Ynab.Collections;

public record TransactionsByYear(string Year, IEnumerable<Transaction> Transactions);