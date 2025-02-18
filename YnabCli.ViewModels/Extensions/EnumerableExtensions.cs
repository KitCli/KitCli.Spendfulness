using YnabCli.ViewModels.ViewModels;

namespace YnabCli.ViewModels.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<TSource> OrderBySortOrder<TSource, TKey>(
        this IEnumerable<TSource> source, ViewModelSortOrder sortOrder, Func<TSource, TKey> sortFunc)
        => sortOrder == ViewModelSortOrder.Ascending
            ? source.OrderBy(sortFunc)
            : source.OrderByDescending(sortFunc);
}