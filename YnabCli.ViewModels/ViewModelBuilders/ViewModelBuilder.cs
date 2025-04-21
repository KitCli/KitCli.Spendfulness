using YnabCli.Aggregation.Aggregator;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.ViewModels.ViewModelBuilders;

public abstract class ViewModelBuilder<TAggregation> : IViewModelBuilder<TAggregation>
    where TAggregation : notnull
{
    protected ViewModelSortOrder ViewModelSortOrder = ViewModelSortOrder.Ascending;
    private Aggregator<TAggregation>? _aggregator;
    private bool _showRowCount = true;

    public IViewModelBuilder<TAggregation> WithAggregator(Aggregator<TAggregation> aggregator)
    {
        _aggregator = aggregator;
        return GetCurrentBuilder();
    }

    public IViewModelBuilder<TAggregation> WithSortOrder(ViewModelSortOrder viewModelSortOrder)
    {
        ViewModelSortOrder = viewModelSortOrder;
        return GetCurrentBuilder();
    }

    public IViewModelBuilder<TAggregation> WithRowCount(bool showRowCount)
    {
        _showRowCount = showRowCount;
        return GetCurrentBuilder();
    }

    public ViewModel Build()
    {
        if (_aggregator is null)
        {
            // This is genuinely an exceptional circumstance.
            throw new InvalidOperationException("You must provide at least one aggregator");
        }

        var evaluation = _aggregator.Aggregate();

        var columns = BuildColumnNames(evaluation);
        var rows = BuildRows(evaluation);

        return BuildViewModel(columns, rows);
    }
    
    protected virtual List<string> BuildColumnNames(TAggregation evaluation) => [];
    
    protected abstract List<List<object>> BuildRows(TAggregation aggregates);

    private ViewModel BuildViewModel(List<string> columnNames, List<List<object>> rows)
    {
        return new ViewModel
        {
            ShowRowCount = _showRowCount,
            Columns = columnNames,
            Rows = rows,
        };
    }

    private IViewModelBuilder<TAggregation> GetCurrentBuilder()
    {
        if (this is not IViewModelBuilder<TAggregation> current)
        {
            throw new Exception("Attempted to return a non-IViewModelBuilder superclass of ViewModelBuilder");
        }
        
        return current;
    }
}