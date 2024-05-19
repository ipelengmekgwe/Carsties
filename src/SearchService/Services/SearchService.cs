using MongoDB.Entities;
using SearchService.Helpers;
using SearchService.Models;

namespace SearchService.Services;

public class SearchService : ISearchService
{
    public async Task<PagedResults<List<Item>>> SearchItems(string searchTerm, int pageNumber = 1, int pageSize = 5)
    {
        var query = DB.PagedSearch<Item>();
        query.Sort(x => x.Ascending(a => a.Make));

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query.Match(Search.Full, searchTerm)
                .SortByTextScore();
        }

        query.PageNumber(pageNumber);
        query.PageSize(pageSize);

        var result = await query.ExecuteAsync();

        return new PagedResults<List<Item>>
        {
            Results = [.. result.Results],
            PageCount = result.PageCount,
            TotalCount = result.TotalCount
        };
    }
}
