using YnabCli.Aggregation.Aggregator;
using YnabCli.ViewModels.ViewModels;

namespace YnabCli.ViewModels.ViewModelBuilders;

public interface IViewModelBuilder<TAggregation> where TAggregation : notnull
{
    IViewModelBuilder<TAggregation> WithAggregator(Aggregator<TAggregation> aggregator);
    
    IViewModelBuilder<TAggregation> WithSortOrder(ViewModelSortOrder viewModelSortOrder);

    IViewModelBuilder<TAggregation> WithRowCount(bool showRowCount);

    ViewModel Build();
}