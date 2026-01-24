using Microsoft.Azure.Cosmos;
using Ynab;

namespace Spendfulness.Database.Cosmos;

public static class TransactionExtensions
{
    public static TransactionEntity ToCosmosTransaction(this Transaction ynabTransaction)
    {
        return new TransactionEntity
        {
            Id = ynabTransaction.Id,
            Occured = ynabTransaction.Occured,
            Memo = ynabTransaction.Memo,
            Amount = ynabTransaction.Amount,
            PayeeId = ynabTransaction.PayeeId,
            PayeeName = ynabTransaction.PayeeName,
            CategoryId = ynabTransaction.CategoryId,
            CategoryName = ynabTransaction.CategoryName,
            IsTransfer = ynabTransaction.IsTransfer,
            AccountId = ynabTransaction.AccountId,
            FlagName = ynabTransaction.FlagName,
            FlagColour = ynabTransaction.FlagColour.ToString(),
            SplitTransactions = ynabTransaction.SplitTransactions?
                .Select(SplitTransactionsExtensions.ToCosmosSplitTransaction)
                .ToList()
        };
    }
    
    public static (string Id, PartitionKey PartitionKey) GetIdentifier(this Transaction transaction) => (transaction.Id, new PartitionKey(transaction.Id));}