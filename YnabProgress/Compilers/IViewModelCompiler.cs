using YnabProgress.ViewModels;

namespace YnabProgress.Compilers;

public interface IViewModelCompiler<in TDataSet> 
{
    public ViewModel Compile(TDataSet data);
}