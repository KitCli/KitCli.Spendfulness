using Ynab.Http;
using Ynab.Responses.Categories;

namespace Ynab.Clients;

public class CategoryClient(YnabHttpClientBuilder builder, string ynabBudgetApiPath) : YnabApiClient
{
    public async Task<IEnumerable<CategoryGroup>> GetCategoryGroups()
    {
        var response = await Get<GetCategoriesResponseData>(string.Empty);
        return response.Data.CategoryGroups.Select(cg => new CategoryGroup(cg));
    }
    
    protected override HttpClient GetHttpClient() => builder.Build(ynabBudgetApiPath, YnabApiPath.Categories);
}