using Ynab;

namespace Spendfulness.Database.Cosmos;

public static class SplitTransactionsExtensions
{
    public static SplitTransactionEntity ToCosmosSplitTransaction(this SplitTransactions ynabSplit)
    {
        return new SplitTransactionEntity
        {
            Id = ynabSplit.Id,
            Occured = ynabSplit.Occured,
            Memo = ynabSplit.Memo,
            Amount = ynabSplit.Amount,
            PayeeId = ynabSplit.PayeeId,
            PayeeName = ynabSplit.PayeeName,
            CategoryId = ynabSplit.CategoryId,
            CategoryName = ynabSplit.CategoryName,
            IsTransfer = ynabSplit.IsTransfer,
            AccountId = ynabSplit.AccountId
        };
    }
}