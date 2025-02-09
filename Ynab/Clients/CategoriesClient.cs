using Ynab.Http;
using Ynab.Responses.Categories;

namespace Ynab.Clients;

public class CategoriesClient(YnabHttpClientFactory ynabHttpClientFactory, string parentApiPath)
    : YnabApiClient
{
    private const string CategoriesApiPath = "categories";

    public async Task<IEnumerable<CategoryGroup>> GetCategoryGroups()
    {
        var response = await Get<GetCategoriesResponseData>(CategoriesApiPath);
        return response.Data.CategoryGroups.Select(cg => new CategoryGroup(this, cg));
    }
    
    protected override HttpClient GetHttpClient() =>
        ynabHttpClientFactory.Create(parentApiPath, CategoriesApiPath);
}