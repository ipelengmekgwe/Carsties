namespace SearchService.Helpers;

public class PagedResults<T>
{
    public T Results { get; set; }
    public int PageCount { get; set; }
    public long TotalCount { get; set; }
}
