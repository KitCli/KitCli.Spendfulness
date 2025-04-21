namespace YnabCli.Aggregation.Aggregates;

// TODO: I hate that this aggregate has a dictionary in it.

[Obsolete("Please do not use this method.")]
public record LegacyCategoryYearAverageAggregate(string CategoryName, Dictionary<string, decimal> AverageAmountByYears);