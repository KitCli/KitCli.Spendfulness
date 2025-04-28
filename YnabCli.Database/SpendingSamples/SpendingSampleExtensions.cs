using Ynab;

namespace YnabCli.Database.SpendingSamples;

public static class SpendingSampleExtensions
{
    /// <summary>
    /// Matches a Transaction based on its payee, most recent price, and used category ids.
    /// </summary>
    ///  TODO: This is for matching un-split transactions with samples.
    public static bool SimilarTo(this SpendingSample sample, Transaction transaction)
    {
        var mostRecentSampleTotal = sample.Matches.Sum(x => x.MostRecentPrice.Amount);
        
        var allCategoryIds = sample.Matches
            .Select(x => x.YnabCategoryId)
            .Distinct();
        
        // Payee is the same, though not sure this matters.
        return sample.YnabPayeeId == transaction.PayeeId && 
               
               // Transaction is categorised like one of the matches
               transaction.CategoryId.HasValue &&
               allCategoryIds.Contains(transaction.CategoryId.Value) &&
               
               // Transaction amount equals or can be inclusive
               mostRecentSampleTotal <= transaction.Amount;
    }
}