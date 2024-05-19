using SearchService.Helpers;
using SearchService.Models;

namespace SearchService.Services
{
    public interface ISearchService
    {
        public Task<PagedResults<List<Item>>> SearchItems(string searchTerm, int pageNumber = 1, int pageSize = 5);
    }
}
