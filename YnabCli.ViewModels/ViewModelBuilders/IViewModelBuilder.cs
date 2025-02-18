using YnabCli.ViewModels.Aggregator;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.ViewModels.ViewModelBuilders;

public interface IViewModelBuilder<in TAggregator, TAggregation>
    where TAggregator : Aggregator<TAggregation>
    where TAggregation : notnull
{
    IViewModelBuilder<TAggregator, TAggregation> AddAggregator(TAggregator aggregator);
    
    IViewModelBuilder<TAggregator, TAggregation> AddColumnNames(List<string> columnNames);
    
    IViewModelBuilder<TAggregator, TAggregation> AddSortOrder(ViewModelSortOrder viewModelSortOrder);

    IViewModelBuilder<TAggregator, TAggregation> AddRowCount(bool showRowCount);

    ViewModel Build();
}