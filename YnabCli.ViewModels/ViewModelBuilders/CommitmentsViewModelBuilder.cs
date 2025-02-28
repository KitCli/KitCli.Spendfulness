using Ynab.Sanitisers;
using YnabCli.Database.Commitments;
using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.Calculators;
using YnabCli.ViewModels.Extensions;
using YnabCli.ViewModels.Formatters;

namespace YnabCli.ViewModels.ViewModelBuilders;

public class CommitmentsViewModelBuilder : ViewModelBuilder<CommitmentsAggregator, IEnumerable<Commitment>>
{
    protected override List<string> BuildColumnNames(IEnumerable<Commitment> evaluation)
        => [
            nameof(Commitment.Name),
            nameof(Commitment.Started),
            nameof(Commitment.RequiredBy),
            nameof(Commitment.Funded),
            nameof(Commitment.Needed),
            "% Met"
        ];

    protected override List<List<object>> BuildRows(IEnumerable<Commitment> aggregates)
        => aggregates
            .OrderBySortOrder(ViewModelSortOrder, c => c.RequiredBy)
            .Select(BuildRow)
            .ToList();

    private List<object> BuildRow(Commitment commitment)
    {
        var displayableStarted = IdentifierSanitiser.SanitiseForMonth(commitment.Started);
        var displayableRequiredBy = IdentifierSanitiser.SanitiseForMonth(commitment.RequiredBy);
        
        var displayableFunded = CurrencyDisplayFormatter.Format(commitment.Funded);
        var displayableNeeded = CurrencyDisplayFormatter.Format(commitment.Needed);
        
        var percentageMet = PercentageCalculator.Calculate(commitment.Funded, commitment.Target);
        var displayablePercentageMet = PercentageDisplayFormatter.Format(percentageMet);

        return
        [
            commitment.Name,
            displayableStarted,
            displayableRequiredBy,
            displayableFunded,
            displayableNeeded,
            displayablePercentageMet
        ];
    }
}