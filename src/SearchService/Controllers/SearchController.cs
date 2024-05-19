using Microsoft.AspNetCore.Mvc;
using SearchService.Helpers;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController(ISearchService serachService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResults<List<Item>>>> SearchItems(string searchTerm, int pageNumber = 1, int pageSize = 5)
    {
        var result = await serachService.SearchItems(searchTerm, pageNumber, pageSize);

        return result;
    }
}
